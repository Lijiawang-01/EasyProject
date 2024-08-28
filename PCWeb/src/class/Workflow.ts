//错误列表
export interface ErrorList {
  name: string;
  type: string[];
}
//工作流主流程
export interface Workflow {
  workFlowDef: WorkflowInfo;
  directorMaxLevel: number;
  nodeConfig: NodeConfig;
  tableId: string;
}
//工作流信息
export interface WorkflowInfo {
  name: string;
}
export interface NodeConfigStore {
  value: NodeConfig;
  flag: boolean;
  id: string
}
//节点配置
export interface NodeConfig {
  nodeName: string;
  type: number;
  priorityLevel: number;
  settype: number;
  selectMode: number;
  selectRange: number;
  directorLevel: number;
  examineMode: number;
  noHanderAction: number;
  examineEndDirectorLevel: number;
  ccSelfSelectFlag: number;
  conditionList: ConditionList[];
  nodeUserList: NodeUserList[];
  childNode: NodeConfig;
  conditionNodes: NodeConfig[];
  error: boolean;
  flag: boolean;
  uid: string
}
//条件
export interface ConditionList {
  columnId: number;
  columnName: string;
  type: number;
  optType: number;
  zdy1: string;
  zdy2: string;
  opt1: string;
  opt2: string;
  columnDbname: string;
  columnType: string;
  showType: number;
  showName: string;
  fixedDownBoxValue: string;
}
//用户列表
export interface NodeUserList {
  targetId: number;
  type: number;
  name: string;
}
//选择列表
export interface SelectUser {
  code: string;
  id: number;
  name: string;
  parentId: number;
  parenName: string;
  typeName: string;
}