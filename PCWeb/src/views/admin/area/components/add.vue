<template>
	<el-dialog v-model="isVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
		<el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
			<el-form-item label="菜单名称" prop="areaName">
				<el-input v-model="form.areaName" />
			</el-form-item>
			<el-form-item label="父级菜单" prop="parentId">
				<el-select v-model="form.parentId" clearable filterable>
					<el-option v-for="item in appAreaList" :key="item.id" :label="item.name" :value="item.id" />
				</el-select>
			</el-form-item>
			<el-form-item label="菜单顺序" prop="order">
				<el-input v-model="form.order" />
			</el-form-item>
			<el-form-item label="是否启用" prop="isEnable">
				<el-switch v-model="form.isEnable" />
			</el-form-item>
			<el-form-item label="描述" prop="description">
				<el-input v-model="form.description" />
			</el-form-item>
			<!-- 按钮操作组 -->
			<el-form-item class="btn">
				<el-button type="primary" @click="submit(ruleFormRef)">确定</el-button>
				<el-button @click="close(ruleFormRef)">取消</el-button>
			</el-form-item>
		</el-form>
	</el-dialog>
</template>

<script lang="ts" setup>
import { ref, reactive, computed, defineEmits, onMounted, watch } from 'vue';
import { addArea, editArea, getAreaDataSelect } from '@/api/area/areaApi';
import { AreaModel } from '../class/AreaModel';
import type { FormInstance, FormRules } from 'element-plus';
import { ElMessage } from 'element-plus';
const ruleFormRef = ref<FormInstance>();
const props = defineProps({
	addVisible: Boolean,
	info: AreaModel,
});
const isVisible = ref(props.addVisible);
const form = ref({
	id: props.info?.id,
	areaName: '',
	parentId: '',
	order: 99,
	isEnable: false,
	description: '',
});
const rules = reactive<FormRules>({
	name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
});
//监听
watch(
	() => props,
	(newInfo) => {
		isVisible.value = newInfo.addVisible;
		if (newInfo.info != undefined) {
			form.value = {
				id: newInfo.info.id,
				areaName: newInfo.info.areaName,
				parentId: newInfo.info.parentId,
				order: newInfo.info.displayOrder,
				isEnable: newInfo.info.isEnable,
				description: newInfo.info.description,
			};
		} else {
			form.value = {
				id: '',
				areaName: '',
				parentId: '',
				order: 99,
				isEnable: false,
				description: '',
			};
		}
	},{deep:true}
);
//defineEmits用于定义回调事件，里面是数组，可以定义多个事件
const emits = defineEmits(['CloseAdd']);
const handleClose = () => {
	emits('CloseAdd');
};
const defaultTreeData = ref([
	{
		id: '',
		name: '请选择',
	},
]);
const appAreaList = ref([
	{
		id: '',
		name: '请选择',
	},]);
//读取下拉数据
const treeData = ref();
onMounted(() => {
	LoadMenuData();
});
const LoadMenuData = () => {
	let parms = {
		id: '',
		AreaName: '',
		parentId: '',
		description: '',
		pageIndex: 1,
		pageSize: 10,
	};
	getAreaDataSelect(parms)
		.then((res) => {
			appAreaList.value = res as any;
		})
		.catch((err) => {
			console.log(err);
		});
};
const submit = async (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	var parms = {
		id: form.value.id,
		areaName: form.value.areaName,
		parentId: form.value.parentId,
		order: form.value.order,
		isEnable: form.value.isEnable,
		description: form.value.description,
	};
	if (props.info == undefined) {
		// 执行添加逻辑
		const res = (await addArea(parms)) as any as boolean;
		if (res) {
			ElMessage({
				message: '添加成功！',
				type: 'success',
			});
		}
	} else {
		// 执行修改逻辑
		const res = (await editArea(parms)) as any as boolean;
		if (res) {
			ElMessage({
				message: '编辑成功！',
				type: 'success',
			});
		}
	}
	//添加菜单之后重新加载下拉框
	LoadMenuData();
	ruleFormRef.resetFields();
	emits('CloseAdd');
};
const close = (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	ruleFormRef.resetFields();
	emits('CloseAdd');
};
</script>
