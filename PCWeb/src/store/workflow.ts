import { NodeConfig, NodeConfigStore } from '@/class/Workflow';
import { defineStore } from 'pinia';

const workFlowstore = defineStore('workFlowstore', {
  state: () => ({
    tableId: '',
    isTried: false,
    promoterDrawer: false,
    approverDrawer: false,
    approverConfig1: {} as NodeConfig,
    copyerDrawer: false,
    copyerConfig1: {} as NodeConfig,
    conditionDrawer: false,
    conditionsConfig1: {
      conditionNodes: [] as NodeConfig[],
    } as any as NodeConfig,
  }),
  actions: {
    setTableId(tableId: string) {
      this.tableId = tableId
    },
    setIsTried(isTried: boolean) {
      this.isTried = isTried
    },
    setPromoter(promoterDrawer: boolean) {
      this.promoterDrawer = promoterDrawer
    },
    setApprover(approverDrawer: boolean) {
      this.approverDrawer = approverDrawer
    },
    setApproverConfig(approverConfig1: NodeConfig) {
      this.approverConfig1 = approverConfig1
    },
    setCopyer(copyerDrawer: boolean) {
      this.copyerDrawer = copyerDrawer
    },
    setCopyerConfig(copyerConfig1: NodeConfig) {
      this.copyerConfig1 = copyerConfig1
    },
    setCondition(conditionDrawer: boolean) {
      this.conditionDrawer = conditionDrawer
    },
    setConditionsConfig(conditionsConfig1: NodeConfig) {
      this.conditionsConfig1 = conditionsConfig1
    },
  }
});

export default workFlowstore;
