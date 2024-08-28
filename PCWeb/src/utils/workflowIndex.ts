import { ConditionList, NodeConfig, NodeUserList, SelectUser } from "@/class/Workflow";

export function arrToStr(arr: NodeUserList[]) {
  if (arr) {
    return arr.map(item => { return item.name }).toString()
  }
}
export function toggleClass(arr: SelectUser[], elem: SelectUser) {
  return arr.some(item => { return item.id == elem.id });
}
export function toggleClassColumn(arr: ConditionList[], elem: ConditionList) {
  return arr.some(item => { return item.columnId == elem.columnId });
}
export function toChecked(arr: SelectUser[], elem: SelectUser) {
  const isIncludes = toggleClass(arr, elem);
  !isIncludes ? arr.push(elem) : removeEle(arr, elem);
}
export function toCheckedColumn(arr: ConditionList[], elem: ConditionList) {
  const isIncludes = toggleClassColumn(arr, elem);
  !isIncludes ? arr.push(elem) : removeEle3(arr, elem);
}
export function removeEle(arr: SelectUser[], elem: SelectUser) {
  let includesIndex = -1;
  arr.map((item, index) => {
    if (item.id == elem.id) {
      includesIndex = index
    }
  });
  if (includesIndex >= 0)
    arr.splice(includesIndex, 1);
}
export function removeEle2(arr: NodeUserList[], elem: NodeUserList) {
  let includesIndex = -1;
  arr.map((item, index) => {
    if (item.targetId == elem.targetId) {
      includesIndex = index
    }
  });
  if (includesIndex >= 0)
    arr.splice(includesIndex, 1);
}
export function removeEle3(arr: ConditionList[], elem: ConditionList) {
  let includesIndex = -1;
  arr.map((item, index) => {
    if (item.columnId == elem.columnId) {
      includesIndex = index
    }
  });
  if (includesIndex >= 0)
    arr.splice(includesIndex, 1);
}
export function setApproverStr(nodeConfig: NodeConfig) {
  if (nodeConfig.settype == 1) {
    if (nodeConfig.nodeUserList.length == 1) {
      return nodeConfig.nodeUserList[0].name
    } else if (nodeConfig.nodeUserList.length > 1) {
      if (nodeConfig.examineMode == 1) {
        return arrToStr(nodeConfig.nodeUserList)
      } else if (nodeConfig.examineMode == 2) {
        return nodeConfig.nodeUserList.length + "人会签"
      }
    }
  } else if (nodeConfig.settype == 2) {
    const level = nodeConfig.directorLevel == 1 ? '直接主管' : '第' + nodeConfig.directorLevel + '级主管'
    if (nodeConfig.examineMode == 1) {
      return level
    } else if (nodeConfig.examineMode == 2) {
      return level + "会签"
    }
  } else if (nodeConfig.settype == 4) {
    if (nodeConfig.selectRange == 1) {
      return "发起人自选"
    } else {
      if (nodeConfig.nodeUserList.length > 0) {
        if (nodeConfig.selectRange == 2) {
          return "发起人自选"
        } else {
          return '发起人从' + nodeConfig.nodeUserList[0].name + '中自选'
        }
      } else {
        return "";
      }
    }
  } else if (nodeConfig.settype == 5) {
    return "发起人自己"
  } else if (nodeConfig.settype == 7) {
    return '从直接主管到通讯录中级别最高的第' + nodeConfig.examineEndDirectorLevel + '个层级主管'
  }
}
export function dealStr(str: string, obj: any) {
  const arr: any[] = [];
  const list = str.split(",");
  for (const elem in obj) {
    list.map(item => {
      if (item == elem) {
        arr.push(obj[elem].value)
      }
    })
  }
  return arr.join("或")
}
export function conditionStr(nodeConfig: NodeConfig, index: any) {
  const { conditionList, nodeUserList } = nodeConfig.conditionNodes[index];
  if (conditionList.length == 0) {
    return (index == nodeConfig.conditionNodes.length - 1) && nodeConfig.conditionNodes[0].conditionList.length != 0 ? '其他条件进入此流程' : '请设置条件'
  } else {
    let str = ""
    for (let i = 0; i < conditionList.length; i++) {
      const { columnId, columnType, showType, showName, optType, zdy1, opt1, zdy2, opt2, fixedDownBoxValue } = conditionList[i];
      if (columnId == 0) {
        if (nodeUserList.length != 0) {
          str += '发起人属于：'
          str += nodeUserList.map(item => { return item.name }).join("或") + " 并且 "
        }
      }
      if (columnType == "String" && showType == 3) {
        if (zdy1) {
          str += showName + '属于：' + dealStr(zdy1, JSON.parse(fixedDownBoxValue)) + " 并且 "
        }
      }
      if (columnType == "Double") {
        if (optType != 6 && zdy1) {
          const optTypeStr = ["", "<", ">", "≤", "=", "≥"][optType]
          str += `${showName} ${optTypeStr} ${zdy1} 并且 `
        } else if (optType == 6 && zdy1 && zdy2) {
          str += `${zdy1} ${opt1} ${showName} ${opt2} ${zdy2} 并且 `
        }
      }
    }
    return str ? str.substring(0, str.length - 4) : '请设置条件'
  }
}
export function copyerStr(nodeConfig: NodeConfig) {
  if (nodeConfig.nodeUserList.length != 0) {
    return arrToStr(nodeConfig.nodeUserList)
  } else {
    if (nodeConfig.ccSelfSelectFlag == 1) {
      return "发起人自选"
    }
  }
}
export function toggleStrClass(item: ConditionList, key: any) {
  const a = item.zdy1 ? item.zdy1.split(",") : []
  return a.some(item => { return item == key });
}
const S4 = function () { return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1); };
export function guid() {
  return (S4() + S4() + '-' + S4() + '-' + S4() + '-' + S4() + '-' + S4() + S4() + S4());
}