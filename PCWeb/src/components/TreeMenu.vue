<template>
	<!-- item -->
	<el-menu-item v-for="item in menuList?.filter((s) => s.children == null)" :index="item.index" :key="item">
		<template #title>
			<el-icon>
				<list />
			</el-icon>
			<span>{{ item.name }}</span>
		</template>
	</el-menu-item>
	<!-- sub-item -->
	<el-sub-menu v-for="item in menuList?.filter((s) => s.children != null)" :index="item.index" :key="item">
		<template #title>
			<el-icon>
				<icon-menu />
			</el-icon>
			<span>{{ item.name }}</span>
		</template>
		<TreeMenu :list="item.children"></TreeMenu>
	</el-sub-menu>
</template>
<script lang="ts" setup>
import { List, Menu as IconMenu } from '@element-plus/icons-vue';
import { watch } from 'vue';
import { TreeModel } from '@/class/TreeModel';
const props = defineProps({
	list: Array as () => Array<TreeModel>,
});
let menuList = props.list;
watch(
	() => props.list,
	(val) => {
		menuList = val;
	}
);
</script>
