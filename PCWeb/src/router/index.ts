import { createRouter, createWebHistory } from 'vue-router';
import LoginPage from '@/views/index/LoginPage.vue';
import RootPage from '@/views/index/RootPage.vue';
import DeskTop from '@/views/index/DeskTop.vue';
import Tool from '@/global';
import { UserInfo } from '@/views/index/class/UserInfo';
const routes = [
  {
    path: '/',
    component: RootPage,
    name: 'home',
    children: [
      { name: '工作台', path: '/desktop', component: DeskTop },
      // { name: '个人信息', path: '/person', component: PersonCenter },
    ],
  },
  { path: '/login', name: 'login', component: LoginPage },
];

const toolObj = new Tool();

//创建路由
const router = createRouter({
  history: createWebHistory(),
  routes: routes,
});

//路由守卫
// eslint-disable-next-line @typescript-eslint/no-unused-vars, no-unused-vars
router.beforeEach((to: { path: string; }, form: any) => {
  if (localStorage['nickname'] != undefined) {
    const user: UserInfo = JSON.parse(new Tool().FormatToken(localStorage['token']));
    const expDate = toolObj.FormatDate(user.exp);
    const currDate = toolObj.GetDate();
    if (to.path == '/login') {
      if (expDate >= currDate) {
        return { path: '/desktop' };
      } else {
        toolObj.ClearLocalStorage();
      }
    } else {
      if (expDate < currDate) {
        toolObj.ClearLocalStorage();
        return { path: '/login' };
      }
    }
  } else {
    //避免无限重定向，因此要做个判断
    if (to.path !== '/login') {
      return { path: '/login' };
    }
  }
});
export default router;
