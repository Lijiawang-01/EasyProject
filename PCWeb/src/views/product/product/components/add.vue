<template>
	<el-dialog v-model="isVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
		<el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
			<!-- <el-form-item label="序号" prop="id">
                <el-input v-model="form.id" />
            </el-form-item> -->
			<el-form-item label="名称" prop="productName">
				<el-input v-model="form.productName" />
			</el-form-item>
			<el-form-item label="描述" prop="description">
				<el-input v-model="form.description" />
			</el-form-item>
			<el-form-item label="是否启用" prop="isEnable">
				<el-switch v-model="form.isEnable" />
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
import { ref, reactive,  computed, watch } from 'vue';
import { addUsers, editUsers } from '@/api/user/userApi';
import { ProductModel } from '../class/ProductModels';
import { ElMessage } from 'element-plus';
import type { FormInstance, FormRules } from 'element-plus';
const ruleFormRef = ref<FormInstance>();
const props = defineProps({
	addVisible: Boolean,
	info: ProductModel,
});

let isVisible = computed(() => props.addVisible);
const form = ref({
	id: props.info?.id,
	productName: '',
	isEnable: true,
	description: '',
});
const rules = reactive<FormRules>({
	name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
});
//监听
watch(
	() => props.info,
	(newInfo) => {
		if (newInfo != undefined) {
			let currInfo: ProductModel = JSON.parse(newInfo as any) as ProductModel;
			form.value = {
				id: currInfo.id,
				productName: currInfo.productName,
				description: currInfo.description,
				isEnable: currInfo.isEnable,
			};
		} else {
			form.value = {
				id: '',
				productName: '',
				description: '',
				isEnable: false,
			};
		}
	}
);
//defineEmits用于定义回调事件，里面是数组，可以定义多个事件
const emits = defineEmits(['CloseAdd']);
const handleClose = () => {
	emits('CloseAdd');
};

const submit = async (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	var parms = {
		id: form.value.id,
		productName: form.value.productName,
		isEnable: form.value.isEnable,
		description: form.value.description,
	};
	if (props.info == undefined) {
		// 执行添加逻辑
		const res = (await addUsers(parms)) as any as boolean;
		if (res) {
			ElMessage({
				message: '添加成功！',
				type: 'success',
			});
		}
	} else {
		// 执行修改逻辑
		const res = (await editUsers(parms)) as any as boolean;
		if (res) {
			ElMessage({
				message: '编辑成功！',
				type: 'success',
			});
		}
	}
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
