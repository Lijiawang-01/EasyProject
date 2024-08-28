<template>
	<el-dialog v-model="isVisible" title="设置" width="30%" :before-close="handleClose">
		<div>
			<div class="fd-nav">
				<div class="fd-nav-left">
					<div class="fd-nav-back" @click="toReturn">
						<i class="anticon anticon-left"></i>
					</div>
					<div class="fd-nav-title">{{ workFlowDef.name }}</div>
				</div>
				<div class="fd-nav-right">
					<button type="button" class="ant-btn button-publish" @click="saveSet">
						<span>发 布</span>
					</button>
					<el-button @click="close()">取消</el-button>
				</div>
			</div>
			<div class="fd-nav-content">
				<section class="dingflow-design">
					<div class="zoom">
						<div class="zoom-out" :class="nowVal == 50 && 'disabled'" @click="zoomSize(1)"></div>
						<span>{{ nowVal }}%</span>
						<div class="zoom-in" :class="nowVal == 300 && 'disabled'" @click="zoomSize(2)"></div>
					</div>
					<div class="box-scale" :style="`transform: scale(${nowVal / 100});`">
						<nodeWrap v-model:nodeConfigs="workflowNodeConfig" />
						<div class="end-node">
							<div class="end-node-circle"></div>
							<div class="end-node-text">流程结束</div>
						</div>
					</div>
				</section>
			</div>
			<errorDialog v-model:visible="tipVisible" :list="tipList" />
			<approverDrawer :directorMaxLevel="directorMaxLevel" />
			<copyerDrawer />
			<conditionDrawer />
		</div>
	</el-dialog>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch, computed } from 'vue';
import { useRoute } from 'vue-router';
import { ElMessage } from 'element-plus';
import { getWorkFlowData, setWorkFlowData } from '@/api/workflow/index';
import workflowStore from '@/store/workflow';
import errorDialog from '@/components/workflow/dialog/errorDialog.vue';
import approverDrawer from '@/components/workflow/drawer/approverDrawer.vue';
import copyerDrawer from '@/components/workflow/drawer/copyerDrawer.vue';
import conditionDrawer from '@/components/workflow/drawer/conditionDrawer.vue';
import { ErrorList, NodeConfig, Workflow, WorkflowInfo } from '@/class/workflow';
import '@/assets/main.css';
import '@/assets/css/override-element-ui.css';

const props = defineProps({
	setVisible: Boolean,
	flowId: String,
});
let { setTableId, setIsTried } = workflowStore();

let isVisible = computed(() => props.setVisible);
let tipList: ErrorList[] = [];
let tipVisible = ref(false);
let nowVal = ref(100);
let processConfig = {} as Workflow;
let workflowNodeConfig = ref({} as NodeConfig);
let workFlowDef = {} as WorkflowInfo;
let directorMaxLevel = ref(0);
onMounted(() => {
	let route = useRoute();
	getWorkFlowData({ workFlowDefId: props.flowId }).then((data) => {
		processConfig = data as any as Workflow;
		let { nodeConfig: nodes, directorMaxLevel: directors, workFlowDef: works, tableId } = processConfig;
		workflowNodeConfig.value = nodes;
		directorMaxLevel.value = directors;
		workFlowDef = works;
		setTableId(tableId);
	});
});
const toReturn = () => {
	//window.location.href = ""
};
const reErr = ({ childNode }: any) => {
	if (childNode) {
		let { type, error, nodeName, conditionNodes } = childNode;
		if (type == 1 || type == 2) {
			if (error) {
				tipList.push({
					name: nodeName,
					type: ['', '审核人', '抄送人'],
				});
			}
			reErr(childNode);
		} else if (type == 3) {
			reErr(childNode);
		} else if (type == 4) {
			reErr(childNode);
			for (var i = 0; i < conditionNodes.length; i++) {
				if (conditionNodes[i].error) {
					tipList.push({ name: conditionNodes[i].nodeName, type: ['条件'] });
				}
				reErr(conditionNodes[i]);
			}
		}
	} else {
		childNode = null;
	}
};

//监听
watch(
	() => props.flowId,
	(newId) => {
		if (newId != undefined) {
			console.log(newId);
		}
	}
);
//defineEmits用于定义回调事件，里面是数组，可以定义多个事件
const emits = defineEmits(['CloseSet']);
const handleClose = () => {
	emits('CloseSet');
};
const close = () => {
	emits('CloseSet');
};
const saveSet = async () => {
	console.log(processConfig);
	setIsTried(true);
	tipList = [];
	reErr(workflowNodeConfig.value);
	if (tipList.length != 0) {
		tipVisible.value = true;
		return;
	}
	// eslint-disable-next-line no-console
	console.log('设置成功');

	// let res = await setWorkFlowData(processConfig);
	// if ((res as any).code == 200) {
	// 	ElMessage.success('设置成功');
	// 	setTimeout(function () {
	// 		window.location.href = '';
	// 	}, 200);
	// }
	emits('CloseSet');
};
const zoomSize = (type: number) => {
	if (type == 1) {
		if (nowVal.value == 50) {
			return;
		}
		nowVal.value -= 10;
	} else {
		if (nowVal.value == 300) {
			return;
		}
		nowVal.value += 10;
	}
};
</script>
<style>
@import '@/assets/css/workflow.css';
.error-modal-list {
	width: 455px;
}
</style>

<style>
a,
abbr,
acronym,
address,
applet,
article,
aside,
audio,
b,
big,
blockquote,
body,
canvas,
caption,
center,
cite,
code,
dd,
del,
details,
dfn,
div,
dl,
dt,
em,
fieldset,
figcaption,
figure,
footer,
form,
h1,
h2,
h3,
h4,
h5,
h6,
header,
hgroup,
html,
i,
iframe,
img,
ins,
kbd,
label,
legend,
li,
mark,
menu,
nav,
object,
ol,
p,
pre,
q,
s,
samp,
section,
small,
span,
strike,
strong,
sub,
summary,
sup,
table,
tbody,
td,
tfoot,
th,
thead,
time,
tr,
tt,
u,
ul,
var,
video {
	margin: 0;
	padding: 0;
	border: 0;
	outline: 0;
	font-size: 100%;
	font: inherit;
	vertical-align: baseline;
}

.clear:before,
.clear:after {
	content: ' ';
	display: table;
}

.clear:after {
	clear: both;
}

.ellipsis {
	overflow: hidden;
	text-overflow: ellipsis;
	white-space: nowrap;
}

.l {
	float: left;
}

input {
	text-indent: 10px;
}

select {
	text-indent: 8px;
}

.ml_10 {
	margin-left: 10px;
}

.mr_10 {
	margin-right: 10px;
}

.radio_box a,
.check_box a {
	font-size: 12px;
	position: relative;
	padding-left: 20px;
	margin-right: 30px;
	cursor: pointer;
	color: #333;
	white-space: pre;
}

.check_box.not a:hover {
	color: #333;
}

.check_box.not a::before,
.check_box.not a:hover::before {
	border: none;
}

.check_box.not.active {
	background: #f3f3f3;
}

.radio_box a:hover::before,
.check_box a:hover::before {
	border: 1px solid #46a6fe;
}

.radio_box a::before,
.check_box a::before {
	position: absolute;
	width: 14px;
	height: 14px;
	border: 1px solid #dcdfe6;
	border-radius: 2px;
	left: 0;
	top: 1px;
	content: '';
}

.radio_box a::before {
	border-radius: 50%;
}

.check-dot.active::after,
.radio_box a.active::after,
.check_box a.active::after {
	position: absolute;
	width: 10px;
	height: 10px;
	border-radius: 50%;
	top: 3px;
	left: 3px;
	content: '';
}

.radio_box a.active::after {
	background: #46a6fe;
}

.check_box a.active::after {
	background: url('@/assets/images/check_box.png') no-repeat center;
}
</style>
