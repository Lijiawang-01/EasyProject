//router/index.ts

import { UserInfo } from "@/class/UserInfo";
import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../views/Home.vue"
import Index from "../views/Index.vue"
import Tool from '@/global';

const toolObj = new Tool();

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    component: Home,
    redirect: '/index',   //路由重定向
    children: [           //路由嵌套
      {
        path: 'index',
        name: 'Index',
        component: Index
      },
      {
        name: 'Wash',
        path: 'wash',
        component: () => import('../views/Wash.vue')
      },
      {
        name: 'Order',
        path: 'order',
        component: () => import('../views/Order.vue')
      },
      {
        name: 'My',
        path: 'my',
        component: () => import('../views/My.vue')
      }
    ]
  },
  {
    name: 'Login',
    path: '/login',
    component: () => import('../views/Login.vue')
  },
  {
    name: "404",
    path: "/:pathMatch(.*)*",
    component: () => import('../views/404.vue')
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  next();
  // if (localStorage['nickname'] != undefined) {
  //   const user: UserInfo = JSON.parse(new Tool().FormatToken(localStorage['token']));
  //   const expDate = toolObj.FormatDate(user.exp);
  //   const currDate = toolObj.GetDate();
  //   if (to.path == '/login') {
  //     if (expDate >= currDate) {
  //       next();
  //     } else {
  //       toolObj.ClearLocalStorage();
  //       return { path: '/login' };
  //     }
  //   } else {
  //     if (expDate < currDate) {
  //       toolObj.ClearLocalStorage();
  //       return { path: '/login' };
  //     }
  //   }
  // } else {
  //   //避免无限重定向，因此要做个判断
  //   if (to.path !== '/login') {
  //     return { path: '/login' };
  //   }
  // }
})

export default router