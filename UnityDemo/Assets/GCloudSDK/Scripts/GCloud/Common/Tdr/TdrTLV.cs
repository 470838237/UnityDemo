/* This file is generated by tdr. */
/* No manual modification is permitted. */

/* creation time: Wed Jan 14 13:34:07 2015 */
/* tdr version: 2.7.4, build at 20150114 */


using System;

namespace GCloudTdr
{

public class TdrTLV
{
    public readonly static int TLV_MSG_MAGIC_SIZE = 1;

    public readonly static int TLV_MSG_MIN_SIZE = 5;

    public enum TLV_MAGIC
    {
        TLV_MAGIC_VARINT = 0xAA,
        TLV_MAGIC_NOVARINT = 0x99,
    }

    public enum TdrTLVTypeId
    {
        TDR_TYPE_ID_VARINT = 0,
        TDR_TYPE_ID_1_BYTE = 1,
        TDR_TYPE_ID_2_BYTE = 2,
        TDR_TYPE_ID_4_BYTE = 3,
        TDR_TYPE_ID_8_BYTE = 4,
        TDR_TYPE_ID_LENGTH_DELIMITED = 5,
    }

    public static UInt32 makeTag(int id, TdrTLVTypeId type)
    {
        return (UInt32)(id << 4 | (Int32)type);
    }

    public static UInt32 getFieldId(UInt32 tagid)
    {
        return tagid >> 4;
    }

    public static UInt32 getTypeId(UInt32 tagid)
    {
        return tagid & 0xF;
    }

    public static Int32 getMsgSize(ref Byte[] buffer, Int32 size)
    {
        if (null == buffer || size < TLV_MSG_MIN_SIZE)
        {
            return -1;
        }

        Int32 iMsgSize = 0;
        TdrReadBuf srcBuf = new TdrReadBuf(ref buffer, size);
        srcBuf.readInt32(ref iMsgSize, TLV_MSG_MAGIC_SIZE);

        return iMsgSize;
    }

    public static TdrError.ErrorType skipUnknownFields(ref TdrReadBuf srcBuf, TdrTLVTypeId type_id)
    {
        TdrError.ErrorType ret = TdrError.ErrorType.TDR_NO_ERROR;

        switch (type_id)
        {
            case TdrTLVTypeId.TDR_TYPE_ID_VARINT:
                {
                    Int64 tmp = 0;
                    ret = srcBuf.readVarInt64(ref tmp);
                    break;
                }

            case TdrTLVTypeId.TDR_TYPE_ID_1_BYTE:
                {
                    ret = srcBuf.skipForward(1);
                    break;
                }

            case TdrTLVTypeId.TDR_TYPE_ID_2_BYTE:
                {
                    ret = srcBuf.skipForward(2);
                    break;
                }

            case TdrTLVTypeId.TDR_TYPE_ID_4_BYTE:
                {
                    ret = srcBuf.skipForward(4);
                    break;
                }

            case TdrTLVTypeId.TDR_TYPE_ID_8_BYTE:
                {
                    ret = srcBuf.skipForward(8);
                    break;
                }

            case TdrTLVTypeId.TDR_TYPE_ID_LENGTH_DELIMITED:
                {
                    Int32 iLength = 0;
                    ret = srcBuf.readInt32(ref iLength);
                    if (TdrError.ErrorType.TDR_NO_ERROR != ret)
                    {
                        return ret;
                    }

                    ret = srcBuf.skipForward(iLength);
                    break;
                }

            default:
                {
                    ret = TdrError.ErrorType.TDR_ERR_UNKNOWN_TYPE_ID;
                    break;
                }
        }

        return ret;
    }
}

}
