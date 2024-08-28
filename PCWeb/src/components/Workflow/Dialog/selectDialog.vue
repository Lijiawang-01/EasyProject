<template>
	<el-dialog title="选择成员" v-model="visibleDialog" append-to-body class="promoter_person">
		<div class="person_body clear">
			<div class="person_tree l">
				<input type="text" placeholder="搜索成员" v-model="searchVal" @input="getDebounceData(searchVal, activeName)" />
				<el-tabs v-model="activeName" @tab-change="handleClick">
					<el-tab-pane label="选择数据" name="1"></el-tab-pane>
				</el-tabs>
				<p class="ellipsis tree_nav" v-if="activeName === 1 && !searchVal">
					<span v-for="(item, index) in departments.titleDepartments" class="ellipsis" :key="index + 'a'" @click="getDepartmentList(item.id)">{{
						item.name
					}}</span>
				</p>
				<selectBox :list="list" style="height: 360px" />
			</div>
			<selectResult :total="total" @del="delList" :list="resList" />
		</div>
		<template #footer>
			<el-button @click="emits('update:visible', false)">取 消</el-button>
			<el-button type="primary" @click="saveDialog">确 定</el-button>
		</template>
	</el-dialog>
</template>

<script lang="ts" setup>
import selectBox from '../selectBox.vue';
import selectResult from '../selectResult.vue';
import { computed, watch, ref } from 'vue';
import { removeEle, toChecked, toggleClass } from '@/utils/workflowIndex';
import { departments, roles, getDebounceData, getRoleList, getDepartmentList, searchVal } from '@/api/workflow/index';
import { NodeUserList, SelectUser } from '@/class/workflow';
let props = defineProps({
	visible: {
		type: Boolean,
		default: false,
	},
	data: {
		type: Array<NodeUserList>,
		default: () => [],
	},
	isDepartment: {
		type: Boolean,
		default: false,
	},
	activeNames: {
		type: Number,
		default: 1,
	},
});
let emits = defineEmits(['update:visible', 'change']);
let visibleDialog = computed({
	get() {
		return props.visible;
	},
	set() {
		closeDialog();
	},
});
let checkedRoleList = ref([] as SelectUser[]);
let checkedEmployeesList = ref([] as SelectUser[]);
let checkedDepartmentList = ref([] as SelectUser[]);
let activeName = ref(props.activeNames);
let list = computed(() => {
	if (activeName.value === 2) {
		return [
			{
				type: 'role',
				data: roles.value,
				isActive: (item: any) => toggleClass(checkedRoleList.value, item),
				change: (item: any) => toChecked(checkedRoleList.value, item),
			},
		];
	} else {
		return [
			{
				isDepartment: departments.value.childDepartments.length <= 0,
				type: 'department',
				data: departments.value.childDepartments,
				isActive: (item: any) => toggleClass(checkedDepartmentList.value, item),
				change: (item: any) => toChecked(checkedDepartmentList.value, item),
				next: (item: { id: number | undefined }) => getDepartmentList(item.id),
			},
			{
				type: 'employee',
				data: departments.value.employees,
				isActive: (item: any) => toggleClass(checkedEmployeesList.value, item),
				change: (item: any) => toChecked(checkedEmployeesList.value, item),
			},
		];
	}
});
let resList = computed(() => {
	let data = [
		{
			type: 'role',
			data: checkedRoleList.value,
			cancel: (item: any) => removeEle(checkedRoleList.value, item),
		},
		{
			type: 'employee',
			data: checkedEmployeesList.value,
			cancel: (item: any) => removeEle(checkedEmployeesList.value, item),
		},
	];
	if (props.isDepartment) {
		data.splice(1, 0, {
			type: 'department',
			data: checkedDepartmentList.value,
			cancel: (item) => removeEle(checkedDepartmentList.value, item),
		});
	}
	return data;
});
watch(
	() => props.visible,
	(val) => {
		if (val) {
			activeName.value = props.activeNames;
			getDepartmentList();
			searchVal.value = '';
			checkedEmployeesList.value = props.data
				.filter((item) => item.type === 1)
				.map(({ name, targetId }) => ({
					name: name,
					id: targetId,
				})) as SelectUser[];
			checkedRoleList.value = props.data
				.filter((item) => item.type === 2)
				.map(({ name, targetId }) => ({
					name: name,
					id: targetId,
				})) as SelectUser[];
			checkedDepartmentList.value = props.data
				.filter((item) => item.type === 3)
				.map(({ name, targetId }) => ({
					name: name,
					id: targetId,
				})) as SelectUser[];
		}
	}
);
let total = computed(() => {
	return checkedEmployeesList.value.length + checkedRoleList.value.length + checkedDepartmentList.value.length;
});

const handleClick = () => {
	searchVal.value = '';
	if (activeName.value === 1) {
		getDepartmentList();
	} else {
		getRoleList();
	}
};
const saveDialog = () => {
	let checkedList = [...checkedRoleList.value, ...checkedEmployeesList.value, ...checkedDepartmentList.value].map((item) => ({
		type: item.name,
		targetId: item.id,
		name: item.name,
	}));
	emits('change', checkedList);
};
const delList = () => {
	checkedEmployeesList.value = [];
	checkedRoleList.value = [];
	checkedDepartmentList.value = [];
};
const closeDialog = () => {
	emits('update:visible', false);
};
</script>

<style scoped>
@import '@/assets/css/dialog.css';
::deep .customWidth {
	width: 600px !important;
}
</style>
