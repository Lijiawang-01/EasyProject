<template>
	<div class="tag-group my-2 flex flex-wrap gap-1 items-center">
		<!-- a标签href跳转页面会刷新，导致历史tag丢失，所以通过事件触发的机制来实现路由的跳转 -->
		<el-tag
			v-for="tag in dynamicTags"
			:key="tag"
			class="mx-1"
			closable
			:disable-transitions="false"
			type=""
			effect="dark"
			@close="handleClose(tag)"
			@click="goToPath(tag.index)"
		>
			<el-link :underline="false"
				><span class="tagName">{{ tag.name }}</span></el-link
			>
		</el-tag>
	</div>
</template>
<script lang="ts" setup>
import { ref } from 'vue';
import store from '@/store';
import { useRouter } from 'vue-router';
import { TagModel } from '@/class/TagModel';
import { v4 as uuidv4 } from 'uuid';
const router = useRouter();
//页面初始化时，vuex没有值，则这里使用默认的路由当做第一个tag页
if (store().$state.TagArrs.length == 0) {
	let info: TagModel = new TagModel();
	info.index = '/desktop';
	info.name = '工作台';
	store().AddTag(info);
}

const dynamicTags = ref(store().$state.TagArrs);
const handleClose = (tag: TagModel) => {
	dynamicTags.value.splice(dynamicTags.value.indexOf(tag), 1);
};
const goToPath = (index: string) => {
	router.push({ path: index });
};
</script>
<style lang="scss" scoped>
.el-tag {
	margin-left: 5px;

	.tagName {
		color: white !important;
	}
}
</style>
