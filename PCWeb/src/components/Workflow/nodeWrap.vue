<template>
	<div class="node-wrap" v-if="nodeConfig.type < 3">
		<div class="node-wrap-box" :class="(nodeConfig.type == 0 ? 'start-node ' : '') + (isTried && nodeConfig.error ? 'active error' : '')">
			<div class="title" :style="`background: rgb(${bgColors[nodeConfig.type]});`">
				<span v-if="nodeConfig.type == 0">{{ nodeConfig.nodeName }}</span>
				<template v-else>
					<span class="iconfont">{{ nodeConfig.type == 1 ? '' : '' }}</span>
					<input
						v-if="isInput"
						type="text"
						class="ant-input editable-title-input"
						@blur="blurEvent(0)"
						v-model="nodeConfig.nodeName"
						:placeholder="defaultText"
					/>
					<span v-else class="editable-title" @click="clickEvent(0)">{{ nodeConfig.nodeName }}</span>
					<i class="anticon anticon-close close" @click="delNode"></i>
				</template>
			</div>
			<div class="content" @click="setPerson">
				<div class="text">
					<span class="placeholder" v-if="!showText">请选择{{ defaultText }}</span>
					{{ showText }}
				</div>
				<i class="anticon anticon-right arrow"></i>
			</div>
			<div class="error_tip" v-if="isTried && nodeConfig.error">
				<i class="anticon anticon-exclamation-circle"></i>
			</div>
		</div>
		<addNode v-model:childNodeP="nodeConfig.childNode" />
	</div>
	<div class="branch-wrap" v-if="nodeConfig.type == 4">
		<div class="branch-box-wrap">
			<div class="branch-box">
				<button class="add-branch" @click="addTerm">添加条件</button>
				<div class="col-box" v-for="(item, index) in nodeConfig.conditionNodes" :key="index">
					<div class="condition-node">
						<div class="condition-node-box">
							<div class="auto-judge" :class="isTried && item.error ? 'error active' : ''">
								<div class="sort-left" v-if="index != 0" @click="arrTransfer(index, -1)">&lt;</div>
								<div class="title-wrapper">
									<input
										v-if="isInputList[index]"
										type="text"
										class="ant-input editable-title-input"
										@blur="blurEvent(index)"
										v-model="item.nodeName"
									/>
									<span v-else class="editable-title" @click="clickEvent(index)">{{ item.nodeName }}</span>
									<span class="priority-title" @click="setPerson(item.priorityLevel)">优先级{{ item.priorityLevel }}</span>
									<i class="anticon anticon-close close" @click="delTerm(index)"></i>
								</div>
								<div class="sort-right" v-if="index != nodeConfig.conditionNodes.length - 1" @click="arrTransfer(index)">&gt;</div>
								<div class="content" @click="setPerson(item.priorityLevel)">{{ conditionStr(nodeConfig, index) }}</div>
								<div class="error_tip" v-if="isTried && item.error">
									<i class="anticon anticon-exclamation-circle"></i>
								</div>
							</div>
							<addNode v-model:childNodeP="item.childNode" />
						</div>
					</div>
					<nodeWrap v-if="item.childNode" v-model:nodeConfigs="item.childNode" />
					<template v-if="index == 0">
						<div class="top-left-cover-line"></div>
						<div class="bottom-left-cover-line"></div>
					</template>
					<template v-if="index == nodeConfig.conditionNodes.length - 1">
						<div class="top-right-cover-line"></div>
						<div class="bottom-right-cover-line"></div>
					</template>
				</div>
			</div>
			<addNode v-model:childNodeP="nodeConfig.childNode" />
		</div>
	</div>
	<nodeWrap v-if="nodeConfig.childNode" v-model:nodeConfigs="nodeConfig.childNode" />
</template>
<script lang="ts" setup>
import { onMounted, ref, watch, computed } from 'vue';
import { conditionStr, copyerStr, setApproverStr } from '@/utils/workflowIndex';
import workflowStore from '@/store/workflow';
import { bgColors, placeholderList } from '@/utils/workflowConst';
import { NodeConfig } from '@/class/workflow';
import { v4 as uuidv4 } from 'uuid';

let props = defineProps({
	nodeConfigs: {
		type: Object,
		default: () => ({}),
	},
});
let _uid = ref(uuidv4());
let nodeConfig = computed(() => {
	return props.nodeConfigs as NodeConfig;
});
let defaultText = computed(() => {
	return placeholderList[nodeConfig.value.type];
});
let showText = computed(() => {
	if (nodeConfig.value.type == 0) return '发起人';
	if (nodeConfig.value.type == 1) return setApproverStr(nodeConfig.value);
	return copyerStr(nodeConfig.value);
});

let isInputList: boolean[] = [];
let isInput = ref(false);
const resetConditionNodesErr = () => {
	for (var i = 0; i < nodeConfig.value.conditionNodes.length; i++) {
		nodeConfig.value.conditionNodes[i].error = conditionStr(nodeConfig.value, i) == '请设置条件' && i != nodeConfig.value.conditionNodes.length - 1;
	}
};
onMounted(() => {
	if (nodeConfig.value.type == 1) {
		nodeConfig.value.error = !setApproverStr(nodeConfig.value);
	} else if (nodeConfig.value.type == 2) {
		nodeConfig.value.error = !copyerStr(nodeConfig.value);
	} else if (nodeConfig.value.type == 4) {
		resetConditionNodesErr();
	}
});
let emits = defineEmits(['update:nodeConfigs']);
let store = workflowStore();
let { setPromoter, setApprover, setCopyer, setCondition, setApproverConfig, setCopyerConfig, setConditionsConfig } = store;
let isTried = computed(() => store.isTried);
let approverConfig1 = computed(() => store.approverConfig1);
let copyerConfig1 = computed(() => store.copyerConfig1);
let conditionsConfig1 = computed(() => store.conditionsConfig1);

watch(approverConfig1, (approver) => {
	if (approver.flag && approver.uid === _uid.value) {
		emits('update:nodeConfigs', approver);
	}
});
watch(copyerConfig1, (copyer) => {
	if (copyer.flag && copyer.uid === _uid.value) {
		emits('update:nodeConfigs', copyer);
	}
});
watch(conditionsConfig1, (condition) => {
	if (condition.flag && condition.uid === _uid.value) {
		emits('update:nodeConfigs', condition);
	}
});
const clickEvent = (index: number) => {
	if (index || index === 0) {
		isInputList[index] = true;
	} else {
		isInput.value = true;
	}
};
const blurEvent = (index: number) => {
	if (index || index === 0) {
		isInputList[index] = false;
		nodeConfig.value.conditionNodes[index].nodeName = nodeConfig.value.conditionNodes[index].nodeName || '条件';
	} else {
		isInput.value = false;
		nodeConfig.value.nodeName = (nodeConfig.value.nodeName || defaultText) as string;
	}
};
const delNode = () => {
	emits('update:nodeConfigs', nodeConfig.value.childNode);
};
const addTerm = () => {
	let len = nodeConfig.value.conditionNodes.length + 1;
	nodeConfig.value.conditionNodes.push({
		nodeName: '条件' + len,
		type: 3,
		priorityLevel: len,
		conditionList: [],
		nodeUserList: [],
		childNode: {} as NodeConfig,
		settype: 0,
		selectMode: 0,
		selectRange: 0,
		directorLevel: 0,
		examineMode: 0,
		noHanderAction: 0,
		examineEndDirectorLevel: 0,
		ccSelfSelectFlag: 0,
		conditionNodes: [],
		error: false,
		flag: false,
		uid: '',
	});
	resetConditionNodesErr();
	emits('update:nodeConfigs', nodeConfig);
};
const delTerm = (index: number) => {
	nodeConfig.value.conditionNodes.splice(index, 1);
	nodeConfig.value.conditionNodes.map((item, index) => {
		item.priorityLevel = index + 1;
		item.nodeName = `条件${index + 1}`;
	});
	resetConditionNodesErr();
	emits('update:nodeConfigs', nodeConfig);
	if (nodeConfig.value.conditionNodes.length == 1) {
		if (nodeConfig.value.childNode) {
			if (nodeConfig.value.conditionNodes[0].childNode) {
				reData(nodeConfig.value.conditionNodes[0].childNode, nodeConfig.value.childNode);
			} else {
				nodeConfig.value.conditionNodes[0].childNode = nodeConfig.value.childNode;
			}
		}
		emits('update:nodeConfigs', nodeConfig.value.conditionNodes[0].childNode);
	}
};
const reData = (data: any, addData: any) => {
	if (!data.childNode) {
		data.childNode = addData;
	} else {
		reData(data.childNode, addData);
	}
};
const setPerson = (priorityLevel: any) => {
	var { type } = nodeConfig.value;
	if (type == 0) {
		setPromoter(true);
	} else if (type == 1) {
		setApprover(true);
		nodeConfig.value.flag = false;
		nodeConfig.value.uid = _uid.value;
		nodeConfig.value.settype = nodeConfig.value.settype ? nodeConfig.value.settype : 1;
		setApproverConfig(nodeConfig.value);
	} else if (type == 2) {
		setCopyer(true);
		nodeConfig.value.flag = false;
		nodeConfig.value.uid = _uid.value;
		setCopyerConfig(nodeConfig.value);
	} else {
		setCondition(true);
		nodeConfig.value.flag = false;
		nodeConfig.value.uid = _uid.value;
		nodeConfig.value.priorityLevel = priorityLevel;
		setConditionsConfig(nodeConfig.value);
	}
};
const arrTransfer = (index: number, type = 1) => {
	//向左-1,向右1
	nodeConfig.value.conditionNodes[index] = nodeConfig.value.conditionNodes.splice(index + type, 1, nodeConfig.value.conditionNodes[index])[0];
	nodeConfig.value.conditionNodes.map((item, index) => {
		item.priorityLevel = index + 1;
	});
	resetConditionNodesErr();
	emits('update:nodeConfigs', nodeConfig);
};
</script>
<style>
.error_tip {
	position: absolute;
	top: 0px;
	right: 0px;
	transform: translate(150%, 0px);
	font-size: 24px;
}

.promoter_person .el-dialog__body {
	width: 600px !important;
	padding: 10px 20px 14px 20px;
}

.selected_list {
	margin-bottom: 20px;
	line-height: 30px;
}

.selected_list span {
	margin-right: 10px;
	padding: 3px 6px 3px 9px;
	line-height: 12px;
	white-space: nowrap;
	border-radius: 2px;
	border: 1px solid rgba(220, 220, 220, 1);
}

.selected_list img {
	margin-left: 5px;
	width: 7px;
	height: 7px;
	cursor: pointer;
}
</style>
