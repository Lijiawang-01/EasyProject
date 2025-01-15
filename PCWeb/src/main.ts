import { createApp } from 'vue';
import 'element-plus/dist/index.css';
import './assets/css/global.scss';
import './assets/css/custom.scss';
//import DataV, { setClassNamePrefix } from '@dataview/datav-vue3';
import App from './App.vue';
import * as ElementPlusIconsVue from '@element-plus/icons-vue';
import router from './router';
import { createPinia } from 'pinia';
import piniaPlugPersist from 'pinia-plugin-persist';
import ElementPlus from 'element-plus';
import i18n from './local/i18n';
import store from '@/store';
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
// 仅在生产环境中禁用警告
if (process.env.NODE_ENV === 'production') {
  app.config.warnHandler = () => {
    // 忽略警告，不输出到控制台
  };
}

app.mount('#app');

