<template>
	<van-list v-model:loading="loading" :finished="finished" finished-text="没有更多了" @load="onLoad">
		<van-cell v-for="item in list" :key="item" :title="item">
			<van-swipe-cell>
				<div class="van-ellipsis">这是一段最多显示一行的文字，多余的内容会被省略</div>
				<template #left>
					<van-button square type="primary" text="选择" />
				</template>
				<van-cell :border="false" title="单元格" value="内容" />
				<template #right>
					<van-button square type="danger" text="删除" />
					<van-button square type="primary" text="收藏" />
				</template>
			</van-swipe-cell>
		</van-cell>
	</van-list>
	<van-back-top />
</template>
<script setup lang="ts">
import { showToast } from 'vant/lib/toast';
import { ref } from 'vue';
import { useI18n } from 'vue-i18n'; // 多语言

const { t } = useI18n(); // t方法取出，t('code')使用
let list: number[] = [];
const loading = ref(false);
const finished = ref(false);
const onAdd = () => {
	showToast('新增');
};
function onLoad() {
	// 异步更新数据
	// setTimeout 仅做示例，真实场景中一般为 ajax 请求
	setTimeout(() => {
		for (let i = 0; i < 10; i++) {
			list.push(list.length + 1);
		}
		// 加载状态结束
		loading.value = false;
		// 数据全部加载完成
		if (list.length >= 40) {
			finished.value = true;
		}
	}, 1000);
}
</script>
