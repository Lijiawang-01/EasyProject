<template>
	<video
		@mouseenter="handleMouseEnter"
		@mouseleave="handleMouseLeave"
		@mousemove="handleMouseMove"
		style="width: 500px; max-height: 400px"
		ref="videoPlayer"
		class="video-js"
	></video>
</template>

<script setup lang="ts">
import videojs from 'video.js';
import 'video.js/dist/video-js.css';
import { onMounted, ref, onUnmounted } from 'vue';
const props = defineProps({
	src: String,
});
const videoPlayer = ref<any>(null);

const player = ref<any>(null);
onMounted(() => {
	//实例化一个 player 并指定 src
	player.value = videojs(
		videoPlayer.value,
		{
			autoplay: false,
			controls: true,
			sources: [
				{
					src: props.src,
				},
			],
		},
		() => {
			player.value.log('play.....');
		}
	);
});

onUnmounted(() => {
	if (player.value) {
		player.value.dispose();
	}
});
//鼠标移入 视频播放
const handleMouseEnter = () => {
	// if (player.value) {
	//   player.value.play()
	// }
};
//鼠标移出 视频暂停
const handleMouseLeave = () => {
	// if (player.value) {
	//   player.value.pause()
	// }
};
//鼠标移动
const handleMouseMove = (event: any) => {
	//获取当前播放元素的 width
	const videoWidth = player.value.el().clientWidth;
	// layerX 就是鼠标对应的 横轴的数。
	// 获取对应的百分比
	const ratio = event.layerX / videoWidth;
	//获取时长
	const videoDuration = player.value.duration();
	//取末尾两位，因为视频是 精确单位秒 2:00
	player.value.currentTime((videoDuration * ratio).toFixed(2));
};
</script>
