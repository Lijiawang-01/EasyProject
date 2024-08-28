import { createApp } from 'vue';
import 'element-plus/dist/index.css';
import './assets/css/global.scss';
//import DataV, { setClassNamePrefix } from '@dataview/datav-vue3';
import App from './App.vue';
import * as ElementPlusIconsVue from '@element-plus/icons-vue';
import router from './router';
import { createPinia } from 'pinia';
import piniaPlugPersist from 'pinia-plugin-persist';
import ElementPlus from 'element-plus';
import i18n from './local/i18n';
import store from '@/store';
import addNode from '@/components/workflow/addNode.vue';
import nodeWrap from '@/components/workflow/nodeWrap.vue';
const app = createApp(App);

for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
  app.component(key, component);
}
app.use(createPinia().use(piniaPlugPersist));
const addRouter = () => {
  store().addRouter()
}
addRouter()
// router要在添加完路由之后引入，不然还没添加，路由已经生成了
app.use(router);
app.use(i18n);
app.use(ElementPlus);
//app.use(DataV, { classNamePrefix: 'dv-' });
app.mount('#app');

app.component('nodeWrap', nodeWrap); //初始化组件
app.component('addNode', addNode); //初始化组件
