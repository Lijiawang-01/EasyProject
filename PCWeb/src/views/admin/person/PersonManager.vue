<template>
	<el-card class="box-card">
		<template #header>
			<div class="card-header">
				<el-form :inline="true" :model="form" class="demo-form-inline" :rules="rules" ref="ruleFormRef">
					<el-form-item label="名称" prop="name">
						<el-input v-model="form.name" placeholder="请输入名称" />
					</el-form-item>
					<el-form-item> </el-form-item>
					<el-form-item>
						<el-button type="primary" @click="onSubmit(ruleFormRef)">
							<el-icon> <search /> </el-icon>查询
						</el-button>
						<el-button @click="resetForm(ruleFormRef)">
							<el-icon> <refresh-right /> </el-icon>重置
						</el-button>
					</el-form-item>
					<el-form-item style="float: right;"> 
						<el-button type="primary" @click="add">新增</el-button>
						<el-button type="danger" @click="Del">删除</el-button>
						<el-button type="primary" @click="SetingRole">分配角色</el-button>
					</el-form-item>
				</el-form>
			</div>
		</template>
		<el-table :data="tableData" style="width: 100%" ref="multipleTableRef">
			<el-table-column type="selection" width="55" />
			<!-- <el-table-column label="序号" width="70">
				<template #default="scope">
					<div>{{ scope.row.id }}</div>
				</template> 
			</el-table-column>-->
			<el-table-column label="名称" width="100">
				<template #default="scope">
					<div>{{ scope.row.name }}</div>
				</template>
			</el-table-column>
			<el-table-column label="昵称" width="100">
				<template #default="scope">
					<div>{{ scope.row.nickName }}</div>
				</template>
			</el-table-column>
			<el-table-column label="密码">
				<template #default="scope">
					<div>{{ scope.row.password }}</div>
				</template>
			</el-table-column>
			<el-table-column label="创建时间" width="200">
				<template #default="scope">
					<div>
						<span style="margin-left: 10px">{{ scope.row.createDate }}</span>
					</div>
				</template>
			</el-table-column>
			<!-- <el-table-column label="用户类型" width="300">
                <template #default="scope">
                    <div>{{ scope.row.userType }}</div>
                </template>
            </el-table-column> -->
			<el-table-column label="角色" width="300">
				<template #default="scope">
					<div>{{ scope.row.roleName }}</div>
				</template>
			</el-table-column>
			<el-table-column label="描述">
				<template #default="scope">
					<div>{{ scope.row.description }}</div>
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
		<el-pagination background layout="prev, pager, next" :total="form.Total" @current-change="handleCurrentChange" />
		
		<div>
			<form @submit.prevent="uploadFile">
				<input type="file" @change="onFileChange"  />
				<button type="button" @click="uploadFile">上传</button>
			</form>
		</div>
	</el-card>
	<el-dialog v-model="queryDialog" title="详情" width="30%" :before-close="queryDialogClose">
		<table class="queryTable">
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
			<tr>
				<td class="left">列名</td>
				<td class="right">this is name</td>
			</tr>
		</table>
	</el-dialog>
	<addVue :addVisible="addVisible" :info="info" @CloseAdd="CloseAdd"></addVue>
	<setingRole :setingRoleVisible="setingRoleVisible" :personId="personId" @CloseSetingRole="CloseSetingRole"> </setingRole>
</template>
<script lang="ts" setup>
import { reactive, ref, onMounted, toRaw } from 'vue';
import type { ElTable } from 'element-plus';
import type { FormInstance, FormRules } from 'element-plus';
import { ElMessage } from 'element-plus';
import { Search, RefreshRight } from '@element-plus/icons-vue';
import addVue from './components/add.vue';
import setingRole from './components/setingRole.vue';
import { PersonModel } from './class/PersonModel';
import { getUserData, delUsers, batchDelUsers,editUserFile } from '@/api/user/userApi';
const ruleFormRef = ref<FormInstance>();
const multipleTableRef = ref<InstanceType<typeof ElTable>>();
const file = ref(null);
const form = reactive({
	name: '',
	nickName: '',
	userType: 0,
	isEnable: true,
	description: '',
	pageIndex: 1,
	pageSize: 10,
	Total: 0,
});
const rules = reactive<FormRules>({
	name: [{ required: false, message: '请输入名称', trigger: 'blur' }],
});

const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
	if (!ruleFormRef) return;
	await ruleFormRef.validate((valid, fields) => {
		if (valid) {
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
};
const CloseAdd = () => {
	addVisible.value = false;
	info.value = undefined;
	LoadTableData();
};
const info = ref();
const handleEdit = (_index: number, row: PersonModel) => {
	info.value = JSON.stringify(row);
	addVisible.value = true;
};

//分配角色
const setingRoleVisible = ref(false);
const personId = ref();
const SetingRole = () => {
	//获取当前选择的行
	let arr: any[] = multipleTableRef.value?.getSelectionRows();
	if (arr.length == 0) {
		ElMessage({
			message: '请选择需要分配角色的用户！',
			type: 'warning',
		});
	} else if (arr.length > 1) {
		ElMessage({
			message: '只能选择单个用户！',
			type: 'warning',
		});
	} else {
		personId.value = toRaw(arr[0].id);
		setingRoleVisible.value = true;
	}
};
const CloseSetingRole = () => {
	setingRoleVisible.value = false;
	LoadTableData();
};

//单个删除
const handleDelete = async (_index: number, row: PersonModel) => {
	tableData.value = tableData.value?.filter((s) => s.id != row.id);
	const res = (await delUsers(row.id)) as any as boolean;
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
			return "'" + item.id + "'";
		})
		.join(',');
	const res = (await batchDelUsers(ids)) as any as boolean;
	if (res) {
		ElMessage({
			message: '删除成功！',
			type: 'success',
		});
		LoadTableData();
	}
};
const queryDialog = ref(false);
const queryDialogClose = () => {
	queryDialog.value = false;
};
//表格
const tableData = ref<Array<PersonModel>>();
onMounted(async () => {
	await LoadTableData();
});
const LoadTableData = async () => {
	let params = {
		name: form.name,
		nickName: '',
		userType: 1,
		isEnable: form.isEnable,
		description: form.description,
		pageIndex: form.pageIndex,
		pageSize: form.pageSize,
	};
	getUserData(params)
		.then((res) => {
			form.Total = (res as any).total;
			tableData.value = res.data as PersonModel[];
		})
		.catch((err) => {});
};
//分页
const handleCurrentChange = (val: number) => {
	form.pageIndex = val;
	LoadTableData();
};
const onFileChange=(e:any) =>{
      // 获取选中的文件
      const selectedFile = e.target.files[0];
      file.value = selectedFile; // 更新文件数据
};
const uploadFile = async () => {
  const formData = new FormData();
// 假设file.value已经被正确地定义并且可能为null、undefined或者Blob/String
if (file.value ) {
  formData.append('file', file.value);
} else {
  console.warn('Invalid file value, expected string or Blob.');
}
 
  try {
    const response = editUserFile(formData).then((res) => {
		console.log(res);
		})
		.catch((err) => {
    console.warn(err);
});
  } catch (error) {
    console.error(error);
  }
};
</script>

<style lang="scss" scoped>
.el-pagination {
	margin-top: 50px;
}

.cell {
	text-align: center !important;
}

.queryTable {
	width: 500px;
	border-collapse: collapse;

	tr {
		height: 50px;

		td {
			padding: 10px;
		}

		.left {
			width: 30%;
			background-color: #f5f7fa;
		}

		.right {
			width: 80%;
		}
	}
}
</style>
