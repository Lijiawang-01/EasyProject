<template>
	<div class="common-layout">
		<el-container>
			<el-aside class="aside-menu">
				<el-row>
					<el-col :span="24">
						<div class="homepageLogo">
							<ul>
								<!-- <li>
									<el-image style="width: 50px; height: 50px" :src="url" fit="fill" />
								</li> -->
								<li>
									<span>{{ t('message.hello') }}</span>
								</li>
							</ul>
							<el-button type="primary" @click="changeLocal()">多语言</el-button>
						</div>
					</el-col>
				</el-row>
				<el-row class="tac">
					<el-col :span="24">
						<el-menu
							active-text-color="#ffd04b"
							background-color="#545c64"
							class="el-menu-vertical-demo"
							default-active="2"
							text-color="#fff"
							router
							@select="handleSelect"
						>
							<!-- 默认会有个首页的入口 -->
							<el-menu-item index="/desktop">
								<template #title>
									<el-icon>
										<list />
									</el-icon>
									<span>工作台</span>
								</template>
							</el-menu-item>
							<TreeMenuVue :list="res"></TreeMenuVue>
						</el-menu>
					</el-col>
				</el-row>
			</el-aside>
			<el-container>
				<el-header>
					<HeaderCom></HeaderCom>
				</el-header>
				<el-main>
					<router-view></router-view>
				</el-main>
			</el-container>
		</el-container>
	</div>
</template>
<script setup lang="ts">
import { ref, onMounted } from 'vue';
import HeaderCom from '@/components/HeaderCom.vue';
import TreeMenuVue from '@/components/TreeMenu.vue';
import { useRouter } from 'vue-router';
import store from '@/store';
import { TagModel } from '@/class/TagModel';
import { useI18n } from 'vue-i18n';

const { t } = useI18n();
const url = ref('/images/logo.ico');
url.value = '/images/logo.0606fdd2.png';
const res = ref();
onMounted(async () => {
	res.value = store().$state.permissionList;
});
const router = useRouter();
const handleSelect = (index: string) => {
	//根据index从路由列表中获取name
	let name = router.getRoutes().filter((item) => item.path == '/' + index)[0].name as string;
	let model = new TagModel();
	model.index = index;
	model.name = name;
	store().AddTag(model);
};
let isZh = localStorage.getItem('lang') || 'cn' == 'cn' ? true : false;
function changeLocal() {
	if (isZh) {
		localStorage.setItem('lang', 'en');
		store().SetLocale('en');
		isZh = false;
	} else {
		localStorage.setItem('lang', 'cn');
		store().SetLocale('cn');
		isZh = true;
	}
}
</script>
<style lang="scss" scoped>
.homepageLogo {
	height: 50px;
	line-height: 50px;

	span {
		color: white;
		font-size: 24px;
	}

	ul {
		li {
			float: left;
			margin-left: 5px;
			list-style: none;
		}
	}
}
</style>
