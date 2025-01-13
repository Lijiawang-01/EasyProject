<template>
	<el-card class="box-card">
		<template #header>
			<div class="card-header">
				<el-form :inline="true" :model="form" class="demo-form-inline" :rules="rules" ref="ruleFormRef">
					<el-form-item label="名称" prop="areaName">
						<el-input v-model="form.areaName" placeholder="请输入名称" />
					</el-form-item>
					<el-form-item> </el-form-item>
					<el-form-item>
						<el-button type="primary" @click="onSubmit(ruleFormRef)">
							<el-icon>
								<search />
							</el-icon>查询
						</el-button>
						<el-button @click="resetForm(ruleFormRef)">
							<el-icon> <refresh-right /> </el-icon>重置
						</el-button>
					</el-form-item>
					<el-form-item style="float: right;">
						<el-button type="primary" @click="add">新增</el-button>
						<el-button type="danger" @click="Del">删除</el-button>
					</el-form-item>
				</el-form>
			</div>
		</template>
		<el-table :data="tableData" style="width: 100%" ref="multipleTableRef" row-key="id">
			<el-table-column type="selection" width="55" />
			<el-table-column label="时间" width="200">
				<template #default="scope">
					<div>
						<span style="margin-left: 10px">{{ scope.row.createDate }}</span>
					</div>
				</template>
			</el-table-column>
			<el-table-column label="名称" width="180">
				<template #default="scope">
					<div>{{ scope.row.areaName }}</div>
				</template>
			</el-table-column>
			<el-table-column label="排序">
				<template #default="scope">
					<div>{{ scope.row.order }}</div>
				</template>
			</el-table-column>
			<el-table-column label="是否启用">
				<template #default="scope">
					<div>{{ scope.row.isEnable }}</div>
				</template>
			</el-table-column>
			<el-table-column label="操作" align="right" width="200">
				<template #default="scope">
					<!-- <el-button size="small" type="success" @click="handleQuery(scope.$index, scope.row)">详情</el-button> -->
					<el-button size="small" type="primary" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
					<el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
				</template>
			</el-table-column>
		</el-table>
		<el-pagination background layout=" prev, pager, next, jumper,total, sizes" :total="form.total" v-model:page-size="form.pageSize"
		v-model:current-page="form.pageIndex"  @size-change="handleSizeChange" :page-sizes="[10, 20, 30, 40]"
			@current-change="handleCurrentChange" />
	</el-card>

	<addVue :addVisible="addVisible" :info="info" @CloseAdd="CloseAdd"></addVue>
</template>
<script lang="ts" setup>
import { reactive, ref, onMounted } from 'vue';
import type { ElTable } from 'element-plus';
import type { FormInstance, FormRules } from 'element-plus';
import { ElMessage } from 'element-plus';
import { Search, RefreshRight } from '@element-plus/icons-vue';
import addVue from './components/add.vue';
import { AreaModel } from './class/AreaModel';
import { getAreaDataNew, delArea, batchDelArea } from '@/api/area/areaApi';
const ruleFormRef = ref<FormInstance>();
const multipleTableRef = ref<InstanceType<typeof ElTable>>();
const form = reactive({
	areaName: '',
	isEnable: true,
	description: '',
	pageIndex: 1,
	pageSize: 10,
	total: 0,
});
const rules = reactive<FormRules>({
	name: [{ required: false, message: '请输入名称', trigger: 'blur' }],
});

const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	await ruleFormRef.validate((valid, fields) => {
		if (valid) {
			// tableData.value = tableData.value?.filter(s => s.name == form.name)
			LoadTableData();
		} else {
			ElMessage({
				message: JSON.stringify(fields),
				type: 'error',
			});
		}
	});
};
const resetForm = (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	ruleFormRef.resetFields();
	LoadTableData();
};
const addVisible = ref(false);
const add = () => {
	addVisible.value = true;
	info.value = undefined;
};
const CloseAdd = () => {
	addVisible.value = false;
	info.value = undefined;
	LoadTableData();
};
const info = ref();
const handleEdit = (_index: number, row: AreaModel) => {
	info.value =row;
	addVisible.value = true;
};

//单个删除
const handleDelete = async (_index: number, row: AreaModel) => {
	tableData.value = tableData.value?.filter((s) => s.id != row.id);
	const res = (await delArea(row.id)) as any as boolean;
	if (res) {
		ElMessage({
			message: '删除成功！',
			type: 'success',
		});
	}
};
//批量删除
const Del = async () => {
	let arr: any[] = multipleTableRef.value?.getSelectionRows();
	let ids: string = arr
		.map((item) => {
			return "'" + item.Id + "'";
		})
		.join(',');
	const res = (await batchDelArea(ids)) as any as boolean;
	if (res) {
		ElMessage({
			message: '删除成功！',
			type: 'success',
		});
		LoadTableData();
	}
};
//表格
const tableData = ref<Array<AreaModel>>();
onMounted(async () => {
	LoadTableData();
});
const LoadTableData = async () => {
	let parms = {
		areaName: form.areaName,
		parentId: '00000000-0000-0000-0000-000000000000',
		description: form.description,
		pageIndex: form.pageIndex,
		pageSize: form.pageSize,
	};
	getAreaDataNew(parms)
		.then((res) => {
			form.total = (res as any).total;
			tableData.value = (res as any).data as AreaModel[];
		})
		.catch((err) => {
			ElMessage({
				message: err,
				type: 'error',
			});
		});
};
//分页
const handleCurrentChange = (val: number) => {
	form.pageIndex = val;
	LoadTableData();
};//分页
const handleSizeChange = (val: number) => {
	form.pageSize = val;
	LoadTableData();
};
</script>