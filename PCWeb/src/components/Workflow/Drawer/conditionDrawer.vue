<template>
	<el-drawer
		:append-to-body="true"
		title="条件设置"
		v-model="visible"
		class="condition_copyer"
		:show-close="false"
		:size="550"
		:before-close="saveCondition"
	>
		<template #header="{ titleId, titleClass }">
			<h3 :id="titleId" :class="titleClass">条件设置</h3>
			<select v-model="conditionConfig.priorityLevel" class="priority_level">
				<option v-for="item in conditionsConfig.conditionNodes.length" :value="item" :key="item">优先级{{ item }}</option>
			</select>
		</template>
		<div class="demo-drawer__content">
			<div class="condition_content drawer_content">
				<p class="tip">当审批单同时满足以下条件时进入此流程</p>
				<ul>
					<li v-for="(item, index) in conditionConfig.conditionList" :key="index">
						<span class="ellipsis">{{ item.type == 1 ? '发起人' : item.showName }}：</span>
						<div v-if="item.type == 1">
							<p :class="conditionConfig.nodeUserList.length > 0 ? 'selected_list' : ''" @click.self="addConditionRole" style="cursor: text">
								<span v-for="(item1, index1) in conditionConfig.nodeUserList" :key="index1">
									{{ item1.name }}<img src="@/assets/images/add-close1.png" @click="removeEle2(conditionConfig.nodeUserList, item1)" />
								</span>
								<input type="text" placeholder="请选择具体人员/角色/部门" v-if="conditionConfig.nodeUserList.length == 0" @click="addConditionRole" />
							</p>
						</div>
						<div v-else-if="item.columnType == 'String' && item.showType == 3">
							<p class="check_box">
								<a
									:class="toggleStrClass(item, item1.key) && 'active'"
									@click="toStrChecked(item, item1.key)"
									v-for="(item1, index1) in JSON.parse(item.fixedDownBoxValue)"
									:key="index1"
									>{{ item1.value }}</a
								>
							</p>
						</div>
						<div v-else>
							<p>
								<select v-model="item.optType" :style="'width:' + (item.optType == 6 ? 370 : 100) + 'px'" @change="changeOptType(item)">
									<option v-for="{ value, label } in optTypes" :value="value" :key="value">{{ label }}</option>
								</select>
								<input v-if="item.optType != 6" type="text" :placeholder="'请输入' + item.showName" v-model="item.zdy1" />
							</p>
							<p v-if="item.optType == 6">
								<input type="text" style="width: 75px" class="mr_10" v-model="item.zdy1" />
								<select style="width: 60px" v-model="item.opt1">
									<option v-for="{ value, label } in opt1s" :value="value" :key="value">{{ label }}</option>
								</select>
								<span class="ellipsis" style="display: inline-block; width: 60px; vertical-align: text-bottom">{{ item.showName }}</span>
								<select style="width: 60px" class="ml_10" v-model="item.opt2">
									<option v-for="{ value, label } in opt1s" :value="value" :key="value">{{ label }}</option>
								</select>
								<input type="text" style="width: 75px" v-model="item.zdy2" />
							</p>
						</div>
						<a
							v-if="item.type == 1"
							@click="
								conditionConfig.nodeUserList = [];
								removeEle3(conditionConfig.conditionList, item);
							"
							>删除</a
						>
						<a v-if="item.type == 2" @click="removeEle3(conditionConfig.conditionList, item)">删除</a>
					</li>
				</ul>
				<el-button type="primary" @click="addCondition">添加条件</el-button>
				<el-dialog title="选择条件" v-model="conditionVisible" :width="480" append-to-body class="condition_list">
					<p>请选择用来区分审批流程的条件字段</p>
					<p class="check_box">
						<a :class="toggleClassColumn(conditionList, newCondiftion) && 'active'" @click="toCheckedColumn(conditionList, newCondiftion)">发起人</a>
						<a
							v-for="(item, index) in conditions"
							:key="index"
							:class="toggleClassColumn(conditionList, item) && 'active'"
							@click="toCheckedColumn(conditionList, item)"
							>{{ item.showName }}</a
						>
					</p>
					<template #footer>
						<el-button @click="conditionVisible = false">取 消</el-button>
						<el-button type="primary" @click="sureCondition">确 定</el-button>
					</template>
				</el-dialog>
			</div>
			<employees-role-dialog v-model:visible="conditionRoleVisible" :data="checkedList" @change="sureConditionRole" :isDepartment="true" />
			<div class="demo-drawer__footer clear">
				<el-button type="primary" @click="saveCondition">确 定</el-button>
				<el-button @click="closeDrawer">取 消</el-button>
			</div>
		</div>
	</el-drawer>
</template>
<script lang="ts" setup>
import { ref, watch, computed } from 'vue';
import { conditionStr, toCheckedColumn, toggleStrClass, removeEle3, removeEle2, toggleClassColumn } from '@/utils/workflowIndex';
import workflowStore from '@/store/workflow';
import { optTypes, opt1s } from '@/utils/workflowConst';
import { getConditions } from '@/api/workflow';
import employeesRoleDialog from '../dialog/selectDialog.vue';
import { ConditionList, NodeConfig, NodeUserList } from '@/class/workflow';

let conditionVisible = ref(false);
let conditionsConfig = ref({
	conditionNodes: [] as ConditionList[],
} as any);
let conditionConfig = ref({} as NodeConfig);
let PriorityLevel = ref(0);
let conditions = ref([] as ConditionList[]);
let conditionList = ref([] as ConditionList[]);
let checkedList = ref([] as NodeUserList[]);
let conditionRoleVisible = ref(false);

let store = workflowStore();
let { setCondition, setConditionsConfig } = store;
let tableId = computed(() => store.tableId);
let conditionsConfig1 = computed(() => store.conditionsConfig1);
let conditionDrawer = computed(() => store.conditionDrawer);
let visible = computed({
	get() {
		return conditionDrawer.value;
	},
	set() {
		closeDrawer();
	},
});
let newCondiftion = {
	columnId: 0,
	columnName: '',
	type: 0,
	optType: 0,
	zdy1: '',
	zdy2: '',
	opt1: '',
	opt2: '',
	columnDbname: '',
	columnType: '',
	showType: 0,
	showName: '',
	fixedDownBoxValue: '',
};
watch(
	conditionsConfig1,
	(val) => {
		conditionsConfig.value = val;
		PriorityLevel.value = val.priorityLevel;
		conditionConfig.value = val.priorityLevel
			? conditionsConfig.value.conditionNodes[val.priorityLevel - 1]
			: { nodeUserList: [], conditionList: [] };
	},
	{
		deep: true,
	}
);

const changeOptType = (item: any) => {
	if (item.optType == 1) {
		item.zdy1 = 2;
	} else {
		item.zdy1 = 1;
		item.zdy2 = 2;
	}
};
const toStrChecked = (item: ConditionList, key: any) => {
	let a = item.zdy1 ? item.zdy1.split(',') : [];
	var isIncludes = toggleStrClass(item, key);
	if (!isIncludes) {
		a.push(key);
		item.zdy1 = a.toString();
	} else {
		removeStrEle(item, key);
	}
};
const removeStrEle = (item: { zdy1: string }, key: any) => {
	let a = item.zdy1 ? item.zdy1.split(',') : [];
	let includesIndex = 0;
	a.map((item: any, index: any) => {
		if (item == key) {
			includesIndex = index;
		}
	});
	a.splice(includesIndex, 1);
	item.zdy1 = a.toString();
};
const addCondition = async () => {
	conditionList.value = [];
	conditionVisible.value = true;
	await getConditions({ tableId: tableId.value }).then((data) => {
		conditions.value = data as any;
		if (conditionConfig.value.conditionList) {
			for (var i = 0; i < conditionConfig.value.conditionList.length; i++) {
				var { columnId } = conditionConfig.value.conditionList[i];
				if (columnId == 0) {
					conditionList.value.push({
						columnId: 0,
						type: 0,
						optType: 0,
						zdy1: '',
						zdy2: '',
						opt1: '',
						opt2: '',
						columnDbname: '',
						columnType: '',
						showType: 0,
						showName: '',
						fixedDownBoxValue: '',
						columnName: '',
					});
				} else {
					conditionList.value.push(
						conditions.value.filter((item) => {
							return item.columnId == columnId;
						})[0]
					);
				}
			}
		}
	});
};
const sureCondition = () => {
	//1.弹窗有，外面无+
	//2.弹窗有，外面有不变
	for (var i = 0; i < conditionList.value.length; i++) {
		var { columnId, showName, columnName, showType, columnType, fixedDownBoxValue } = conditionList.value[i];
		if (!conditionConfig.value.conditionList) {
			conditionConfig.value.conditionList = [];
		}
		if (toggleClassColumn(conditionConfig.value.conditionList, conditionList.value[i])) {
			continue;
		}
		if (columnId == 0) {
			conditionConfig.value.nodeUserList = [];
			conditionConfig.value.conditionList.push({
				type: 1,
				columnId: columnId,
				showName: '发起人',
				columnName: '',
				optType: 0,
				zdy1: '',
				zdy2: '',
				opt1: '',
				opt2: '',
				columnDbname: '',
				columnType: '',
				showType: 0,
				fixedDownBoxValue: '',
			});
		} else {
			if (columnType == 'Double') {
				conditionConfig.value.conditionList.push({
					showType: showType,
					columnId: columnId,
					type: 2,
					showName: showName,
					optType: 1,
					zdy1: '2',
					opt1: '<',
					zdy2: '',
					opt2: '<',
					columnDbname: columnName,
					columnType: columnType,
					columnName: '',
					fixedDownBoxValue: '',
				});
			} else if (columnType == 'String' && showType == 3) {
				conditionConfig.value.conditionList.push({
					showType: showType,
					columnId: columnId,
					type: 2,
					showName: showName,
					zdy1: '',
					columnDbname: columnName,
					columnType: columnType,
					fixedDownBoxValue: fixedDownBoxValue,
					columnName: '',
					optType: 0,
					zdy2: '',
					opt1: '',
					opt2: '',
				});
			}
		}
	}
	//3.弹窗无，外面有-
	for (let i = conditionConfig.value.conditionList.length - 1; i >= 0; i--) {
		if (!toggleClassColumn(conditionList.value, conditionConfig.value.conditionList[i])) {
			conditionConfig.value.conditionList.splice(i, 1);
		}
	}
	conditionConfig.value.conditionList.sort(function (a: { columnId: number }, b: { columnId: number }) {
		return a.columnId - b.columnId;
	});
	conditionVisible.value = false;
};
const saveCondition = () => {
	closeDrawer();
	var a = conditionsConfig.value.conditionNodes.splice(PriorityLevel.value - 1, 1); //截取旧下标
	conditionsConfig.value.conditionNodes.splice(conditionConfig.value.priorityLevel - 1, 0, a[0]); //填充新下标
	conditionsConfig.value.conditionNodes.map((item: any, index: number) => {
		item.priorityLevel = index + 1;
	});
	for (var i = 0; i < conditionsConfig.value.conditionNodes.length; i++) {
		conditionsConfig.value.conditionNodes[i].error =
			conditionStr(conditionsConfig.value, i) == '请设置条件' && i != conditionsConfig.value.conditionNodes.length - 1;
	}
	conditionsConfig.value.flag = true;
	setConditionsConfig(conditionsConfig.value);
};
const addConditionRole = () => {
	conditionRoleVisible.value = true;
	checkedList.value = conditionConfig.value.nodeUserList;
};
const sureConditionRole = (data: any) => {
	conditionConfig.value.nodeUserList = data;
	conditionRoleVisible.value = false;
};
const closeDrawer = () => {
	setCondition(false);
};
</script>
<style lang="scss" scoped>
.condition_copyer {
	.priority_level {
		position: absolute;
		top: 11px;
		right: 30px;
		width: 100px;
		height: 32px;
		background: rgba(255, 255, 255, 1);
		border-radius: 4px;
		border: 1px solid rgba(217, 217, 217, 1);
		font-size: 12px;
	}

	.condition_content {
		padding: 20px 20px 0;

		p.tip {
			margin: 20px 0;
			width: 510px;
			text-indent: 17px;
			line-height: 45px;
			background: rgba(241, 249, 255, 1);
			border: 1px solid rgba(64, 163, 247, 1);
			color: #46a6fe;
			font-size: 14px;
		}

		ul {
			max-height: 500px;
			overflow-y: scroll;
			margin-bottom: 20px;

			li {
				& > span {
					float: left;
					margin-right: 8px;
					width: 70px;
					line-height: 32px;
					text-align: right;
				}

				& > div {
					display: inline-block;
					width: 370px;

					& > p:not(:last-child) {
						margin-bottom: 10px;
					}
				}

				&:not(:last-child) > div > p {
					margin-bottom: 20px;
				}

				& > a {
					float: right;
					margin-right: 10px;
					margin-top: 7px;
				}

				select,
				input {
					width: 100%;
					height: 32px;
					background: rgba(255, 255, 255, 1);
					border-radius: 4px;
					border: 1px solid rgba(217, 217, 217, 1);
				}

				select + input {
					width: 260px;
				}

				select {
					margin-right: 10px;
					width: 100px;
				}

				p.selected_list {
					padding-left: 10px;
					border-radius: 4px;
					min-height: 32px;
					border: 1px solid rgba(217, 217, 217, 1);
					word-break: break-word;
				}

				p.check_box {
					line-height: 32px;
				}
			}
		}

		.el-button {
			margin-bottom: 20px;
		}
	}
}

.condition_list {
	.el-dialog__body {
		padding: 16px 26px;
	}

	p {
		color: #666666;
		margin-bottom: 10px;

		& > .check_box {
			margin-bottom: 0;
			line-height: 36px;
		}
	}
}
</style>
