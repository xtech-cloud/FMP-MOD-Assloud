using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace XTC.FMP.MOD.Assloud.App.Service
{
    public class FileSubEntity
    {
        /// <summary>
        /// ���·��
        /// </summary>
        public string path { get; set; } = "";

        /// <summary>
        /// ��ϣֵ
        /// </summary>
        public string hash { get; set; } = "";

        /// <summary>
        /// ��С
        /// </summary>
        public ulong Size { get; set; } = 0;

        /// <summary>
        /// �ⲿֱ�ӷ���·��
        /// </summary>
        /// <remarks>
        /// ��ֵʱΪֱ�Ӵ洢���ļ�������URL���ͻ���ʱʵʱ����
        /// �ǿ�ֵʱΪ�ⲿ�洢���ļ�
        /// </remarks>
        public string Url{ get; set; } = "";
    }
}
