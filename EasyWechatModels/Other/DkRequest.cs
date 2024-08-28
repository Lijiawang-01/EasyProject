using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;
using System.Xml.Linq;

namespace EasyWechatModels.Other
{
    public class DkRequest
    {
        /// <summary>
        /// 采购计划编号
        /// </summary>
        public string RequestCode { get; set; }
        /// <summary>
        /// 采购计划名称
        /// </summary>
        public string RequestName { get; set; }

        // 0标准物料计划、10成本中心计划  
        /// </summary>
        public int RequestType { get; set; }
        /// <summary>
        /// 采购计划备注
        /// </summary>
        public string RequestDesc { get; set; }

        /// <summary>
        /// 采购计划附件，FTP上传文件，此处为附件名称多个使用,分割
        /// </summary>
        public string RequestAttachment { get; set; }

        /// <summary>
        /// 采购计划日期
        /// </summary>
        public DateTime RequestDate { get; set; }

        /// <summary>
        /// 是否紧急采购
        /// </summary>
        public int IsEmergency { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>

        /// <summary>
        /// 采购计划姓名
        /// </summary>
        public string CreateName { get; set; }

        /// <summary>
        /// 采购计划编号
        /// </summary>
        public string CreateCode { get; set; }
        public List<DkRequestDetail> DetailList { get; set; }
    }
    public class DkRequestDetail
    {
        /// <summary>
        /// 申请部门名称
        /// </summary>
        public string ReqFactoryName { get; set; }
        /// <summary>
        /// 收货工厂编码--特定3010
        /// </summary>
        public string RecFactoryCode { get; set; }

        /// <summary>
        /// 需用人姓名
        /// </summary>
        public string ReqEmployeeName { get; set; }
        /// <summary>
        /// 需用人编码
        /// </summary>
        public string ReqEmployeeCode { get; set; }
        /// <summary>
        /// 成本中心编码--成本中心申请必填
        /// </summary>
        public string CostCenterCode { get; set; }

        /// <summary>
        /// 需用日期
        /// </summary>
        public DateTime NeedDate { get; set; }

        /// <summary>
        /// 物料数量
        /// </summary>
        public double MaterialCount { get; set; }
        /// <summary>
        /// 物料组编号
        /// </summary>
        public string MaterialGroupCode { get; set; }

        /// <summary>
        /// 库存地名称--默认 信越综合库	
        /// </summary>
        public string StockAddrName { get; set; }

        /// <summary>
        /// 物料ERP编号
        /// </summary>
        public string ErpMaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        public string MaterialDesc { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string MaterialSpecification { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// 预算价
        /// </summary>
        public double BudgetTotalPrice { get; set; }


        /// <summary>
        /// 采购组编码--默认116
        /// </summary>
        public string PurchaseGroupCode { get; set; }


        /// <summary>
        /// 采购周期(天)
        /// </summary>
        public int PurchasingCycle { get; set; }

        /// <summary>
        /// 明细行附件，FTP上传文件，此处为附件名称多个使用,分割
        /// </summary>
        public string MaterialAttachment { get; set; }

        /// <summary>
        /// 备注（计划全流程）
        /// </summary>
        public string PlanMemo { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode { get; set; }
    }
}
