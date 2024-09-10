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

    //接下来编写一些读写缓冲区数据的方法。 写数据的方法 Write 带有 3 个参数， 该方法会
    //把 bs 从 offset 位置开始的 count 个数据写入缓冲区。 Write方法会判断缓冲区是否有足够的
    //剩余量， 必要时调用 ReSize 方法调整 byte 数组长度。 读方法 Read 也带有3 个参数， 它代
    //表把缓冲区前 count 个数据放到 bs 中， 数据从 bs 的 offset 位置开始放入。 Read 方法会调用
    //CheckAndMoveBytes, 必要时移动数据， 以增加 remain。
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
    /// 小段存储
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
    /// 打印缓冲区 仅为调试
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
        //创建
        ByteArray buff = new ByteArray(8);
        CLog.L("[1 debug]->" + buff.Debug());
        CLog.L("[1 string]->" + buff.ToString());
        //写入
        byte[] wb = new byte[] { 1, 2, 3, 4, 5 };
        buff.Write(wb, 0, 5);
        CLog.L("[2 debug]->" + buff.Debug());
        CLog.L("[2 string]->" + buff.ToString());
        //读取
        byte[] rb = new byte[4];
        buff.Read(rb, 0, 2);
        CLog.L("[3 debug]->" + buff.Debug());
        CLog.L("[3 string]->" + buff.ToString());
        CLog.L("[3 string]->" + BitConverter.ToString(rb));
        //写入,Resize
        wb = new byte[] { 6, 7, 8, 8, 10, 11 };
        buff.Write(wb, 0, 6);
        CLog.L("[4 debug]->" + buff.Debug());
        CLog.L("[4 string]->" + buff.ToString());
    }
}
}