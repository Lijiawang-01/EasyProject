<template>
	<el-dialog v-model="isVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
		<el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
			<el-form-item label="名称" prop="index">
				<el-input v-model="form.title" />
			</el-form-item>
			<el-form-item label="描述" prop="name">
				<el-input v-model="form.desc" />
			</el-form-item>
			<el-form-item label="是否启用" prop="activeStatus">
				<el-switch v-model="form.activeStatus" />
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
import { ref, reactive, defineProps, computed, defineEmits, onMounted, watch } from 'vue';
import { getTemplateData, addTemplate } from '@/api/workflow/workflowApi';
import { WorkflowTmpModel } from '../class/WorkflowTmpModel';
import type { FormInstance, FormRules } from 'element-plus';
import { ElMessage } from 'element-plus';
const ruleFormRef = ref<FormInstance>();
const props = defineProps({
	addVisible: Boolean,
	info: WorkflowTmpModel,
});
let isVisible = computed(() => props.addVisible);
const form = ref({
	id: props.info?.id,
	title: '',
	desc: '',
	activeStatus: false,
});
const rules = reactive<FormRules>({
	name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
});
//监听
watch(
	() => props.info,
	(newInfo) => {
		if (newInfo != undefined) {
			let currInfo: WorkflowTmpModel = JSON.parse(newInfo as any) as WorkflowTmpModel;
			form.value = {
				id: currInfo.id,
				title: currInfo.title,
				desc: currInfo.desc,
				activeStatus: currInfo.activeStatus,
			};
		}
	}
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
//读取下拉数据
const treeData = ref();
onMounted(() => {
	LoadTemplateData();
});
const LoadTemplateData = () => {
	// 	let parms = {
	// 		title: '',
	// 		desc: '',
	// 		activeStatus: false,
	// 		pageIndex: 1,
	// 		pageSize: 10,
	// 	};
	// 	getMenuDataNew(parms)
	// 		.then((res) => {
	// 			treeData.value = defaultTreeData.value.concat(...res.data);
	// 		})
	// 		.catch((err) => {
	// 			console.log(err);
	// 		});
};
const submit = async (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	var parms = {
		id: form.value.id,
		title: form.value.title,
		desc: form.value.desc,
		activeStatus: form.value.activeStatus,
	};
	if (props.info == undefined) {
		// 执行添加逻辑
		const res = (await addTemplate(parms)) as any as boolean;
		if (res) {
			ElMessage({
				message: '添加成功！',
				type: 'success',
			});
		}
	} else {
		// // 执行修改逻辑
		// const res = (await editMenu(parms)) as any as boolean;
		// if (res) {
		// 	ElMessage({
		// 		message: '编辑成功！',
		// 		type: 'success',
		// 	});
		// }
	}
	//添加菜单之后重新加载下拉框
	LoadTemplateData();
	ruleFormRef.resetFields();
	emits('CloseAdd');
};
const close = (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	ruleFormRef.resetFields();
	emits('CloseAdd');
};
</script>
<style lang="scss" scoped>
.form {
	min-height: 500px;

	.btn {
		position: absolute;
		bottom: 10px;
	}
}
</style>
