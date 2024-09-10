using Congroo.Core;
using System;
using UnityEngine;

namespace Congroo.Core
{
    public class ByteArray
{
    const int DEFAULT_SIZE = 1024;
    int initSize = 0;
    public byte[] bytes;
    public int readIdx = 0;
    public int writeIdx = 0;
    private int capacity = 0;

    public int remain { get { return capacity - writeIdx; } }
    public int length { get { return writeIdx - readIdx; } }

    public ByteArray(int size = DEFAULT_SIZE)
    {
        bytes = new byte[size];
        capacity = size;
        initSize = size;
        readIdx = 0;
        writeIdx = 0;
    }

    public ByteArray(byte[] defaultBytes)
    {
        bytes = defaultBytes;
        capacity = defaultBytes.Length;
        initSize = defaultBytes.Length;
        readIdx = 0;
        writeIdx = defaultBytes.Length;
    }

    public void ReSize(int size)
    {
        if (size < length) return;
        if (size < initSize) return;
        int n = 1;
        while (n < size) n *= 2;
        capacity = n;
        byte[] newBytes = new byte[capacity];
        Array.Copy(bytes, readIdx, newBytes, 0, writeIdx - readIdx);
        bytes = newBytes;
        writeIdx = length;
        readIdx = 0;
    }

    public void CheckAndMoveBytes()
    {
        if (length < 8)
            MoveBytes();
    }

    public void MoveBytes()
    {
        Array.Copy(bytes, readIdx, bytes, 0, length);
        writeIdx = length;
        readIdx = 0;
    }

    //��������дһЩ��д���������ݵķ����� д���ݵķ��� Write ���� 3 �������� �÷�����
    //�� bs �� offset λ�ÿ�ʼ�� count ������д�뻺������ Write�������жϻ������Ƿ����㹻��
    //ʣ������ ��Ҫʱ���� ReSize �������� byte ���鳤�ȡ� ������ Read Ҳ����3 �������� ����
    //��ѻ�����ǰ count �����ݷŵ� bs �У� ���ݴ� bs �� offset λ�ÿ�ʼ���롣 Read ���������
    //CheckAndMoveBytes, ��Ҫʱ�ƶ����ݣ� ������ remain��
    public int Write(byte[] bs, int offset, int count)
    {
        if(remain < count)
        {
            ReSize(length + count);
        }
        Array.Copy(bs, offset, bytes, writeIdx, count);
        writeIdx += count;
        CheckAndMoveBytes();
        return count;
    }


    public int Read(byte[] bs, int offset, int count)
    {
        count = Math.Min(count, length);
        Array.Copy(bytes, 0, bs, offset, count);
        readIdx += count;
        CheckAndMoveBytes();
        return count;
    }

    /// <summary>
    /// С�δ洢
    /// </summary>
    /// <returns></returns>
    public Int16 ReadInt16()
    {
        if (length < 2)
            return 0;
        Int16 ret = (Int16)((bytes[1] << 8) | bytes[0]);
        readIdx += 2;
        CheckAndMoveBytes();
        return ret;
    }

    public Int32 ReadInt32()
    {
        if (length < 4)
            return 0;
        Int32 ret = (Int32)((bytes[1] << 24) | (bytes[1] << 16) | (bytes[1] << 8) | bytes[0]);
        readIdx += 4;
        CheckAndMoveBytes();
        return ret;
    }


    /// <summary>
    /// ��ӡ������ ��Ϊ����
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return BitConverter.ToString(bytes, readIdx, length);
    }

    public string Debug()
    {
        return string.Format($"readnIdx: {readIdx} writeIdx: {writeIdx} bytes {BitConverter.ToString(bytes, 0, bytes.Length)}");
    }

    
    public static void Test01()
    {
        //����
        ByteArray buff = new ByteArray(8);
        CLog.L("[1 debug]->" + buff.Debug());
        CLog.L("[1 string]->" + buff.ToString());
        //д��
        byte[] wb = new byte[] { 1, 2, 3, 4, 5 };
        buff.Write(wb, 0, 5);
        CLog.L("[2 debug]->" + buff.Debug());
        CLog.L("[2 string]->" + buff.ToString());
        //��ȡ
        byte[] rb = new byte[4];
        buff.Read(rb, 0, 2);
        CLog.L("[3 debug]->" + buff.Debug());
        CLog.L("[3 string]->" + buff.ToString());
        CLog.L("[3 string]->" + BitConverter.ToString(rb));
        //д��,Resize
        wb = new byte[] { 6, 7, 8, 8, 10, 11 };
        buff.Write(wb, 0, 6);
        CLog.L("[4 debug]->" + buff.Debug());
        CLog.L("[4 string]->" + buff.ToString());
    }
}
}