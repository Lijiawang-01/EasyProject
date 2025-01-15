<template>
	<el-dialog v-model="setingIsVisible" title="分配菜单" width="50%" :before-close="handleClose">
		<div class="content">
			<p>
				<el-tree-v2 :data="treeData" :props="{ value: 'id', label: 'name', children: 'children' }" show-checkbox ref="tree" :height="208" />
			</p>
			<div class="btn">
				<el-button type="primary" @click="submit()">确定</el-button>
				<el-button @click="close()">取消</el-button>
			</div>
		</div>
	</el-dialog>
</template>
<script lang="ts" setup>
import { ref, computed, onMounted, toRaw } from 'vue';
import { getMenuDataNew, settingMenu } from '@/api/menu/menuApi';
import { ElMessage, ElTreeV2 } from 'element-plus';
const props = defineProps({
	setingVisible: Boolean,
	roleId: String, //
});
const tree = ref(ElTreeV2);
let setingIsVisible = computed(() => props.setingVisible);
//读取下拉数据
const treeData = ref();
onMounted(async () => {
	let parms = {
		name: '',
		index: '',
		filePath: '',
		parentId: '',
		description: '',
		pageIndex: 1,
		pageSize: 10,
	};
	getMenuDataNew(parms)
		.then((res) => {
			treeData.value = res.data;
		})
		.catch((err) => {
			console.log(err);
		});
});
const emits = defineEmits(['CloseSeting']);
const handleClose = () => {
	emits('CloseSeting');
};
const close = async () => {
	emits('CloseSeting');
};
const submit = async () => {
	let arr: any[] = [];
	const nodes: [] = tree.value.getCheckedNodes();
	if (nodes.length == 0) {
		ElMessage({
			message: '请选择需要分配的菜单！',
			type: 'warning',
		});
	} else {
		nodes.forEach((element) => {
			arr.push(toRaw(element));
		});
		let mids: string = arr
			.map((item) => {
				return `'${item.id}'`;
			})
			.join(',');
		let res = (await settingMenu(props.roleId as string, mids)) as any as boolean;
		if (res) {
			ElMessage({
				message: '设置成功！',
				type: 'success',
			});
			emits('CloseSeting');
		} else {
			ElMessage({
				message: '设置失败，请联系系统管理员！',
				type: 'error',
			});
		}
	}
};
</script>
