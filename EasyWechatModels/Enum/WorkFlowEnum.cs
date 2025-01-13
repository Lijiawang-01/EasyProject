using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWechatModels.Enum
{
    /// <summary>
    /// 工作流枚举
    /// </summary>
    public class WorkFlowEnum
    {
        /// <summary>
        /// 流程状态
        /// </summary>
    	public enum Status
        {
            /// <summary>
            /// 待提交
            /// </summary>
            WaitSumbit = 0,
            /// <summary>
            /// 待审核 
            /// </summary>
            WaitAudit = 1,
            /// <summary>
            /// 审核中
            /// </summary>
            Auditing = 2,
            /// <summary>
            /// 审核通过结束
            /// </summary>
            Finish = 3,
            /// <summary>
            /// 未通过打回拟制人
            /// </summary>
            NotPass = 4,
            /// <summary>
            /// 流程终止
            /// </summary>
            Stop = 5,
            /// <summary>
            /// 拟制人撤回终止
            /// </summary>
            ReBack = 6
        }
        /// <summary>
        /// 流程类型
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// 普通流程
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 投票流程
            /// </summary>
            Vote = 1
        }
        /// <summary>
        /// 节点状态
        /// </summary>
        public enum NodeStatus
        {
            /// <summary>
            /// 待进入
            /// </summary>
            WaitIn = 0,
            /// <summary>
            /// 审核中
            /// </summary>
            Auditing = 1,
            /// <summary>
            /// 处理完成
            /// </summary>
            Finish = 2,
        }
        /// <summary>
        /// 节点处理人类型
        /// </summary>
        public enum NodeSetType
        {
            /// <summary>
            /// 指定人
            /// </summary>
            SetEmpls = 1,
            /// <summary>
            /// 发起人
            /// </summary>
            Self = 5,
            /// <summary>
            /// 提交人自选
            /// </summary>
            Select = 4,
            /// <summary>
            /// 通过API获取
            /// </summary>
            API = 8,
        }
        /// <summary>
        /// 处理人设置类型
        /// </summary>
        public enum NodeUserType
        {
            /// <summary>
            /// 设定部门
            /// </summary>
            SetDeptOID = 3,
            /// <summary>
            /// 创建者公司
            /// </summary>
            WFCreateComId = 4,
            /// <summary>
            /// 上级节点公司
            /// </summary>
            PNodeComID = 5,
            /// <summary>
            /// 角色
            /// </summary>
            RoleID = 2,
            /// <summary>
            /// 创建者上级
            /// </summary>
            WFCreateParentID = 6,
            /// <summary>
            /// 上级节点上级
            /// </summary>
            PNodeParentID = 7
        }
        /// <summary>
        /// 节点类型
        /// </summary>
        public enum NodeType
        {
            /// <summary>
            /// 开始节点
            /// </summary>
            First = 0,
            /// <summary>
            /// 审核节点
            /// </summary>
            Audit = 1,
            /// <summary>
            /// 条件节点
            /// </summary>
            Codition = 3,
            /// <summary>
            /// 路由节点
            /// </summary>
            Route = 4
        }

        public enum NextNodeFlag
        {
            /// <summary>
            /// 审核不通过
            /// </summary>
            NotPass = 2,
            /// <summary>
            /// 流程通过完成
            /// </summary>
            Finish = 3,
            /// <summary>
            /// 已到下个节点
            /// </summary>
            NextNode = 1,
            /// <summary>
            /// 需要选人
            /// </summary>
            WaitSelect = 4,

            /// <summary>
            /// 当前节点没处理完
            /// </summary>
            CurNode = 0
        }

        public enum NodeProcessMethod
        {
            /// <summary>
            /// 竞抢
            /// </summary>
            One = 0,
            /// <summary>
            /// 依次
            /// </summary>
            Round = 1,
            /// <summary>
            /// 并发
            /// </summary>
            All = 2
        }


    }
}
