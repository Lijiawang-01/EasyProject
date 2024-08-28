import useAuthStore from '@/store/index'
import router from '@/router/index'
//使用vite懒加载
const Module = import.meta.glob("../views/**/**/*.vue");
//懒加载组件
function loadComponent(url: string) {
  const url1 = `../views${url}.vue`;
  const path = Module[url1];
  return path;
}
// 处理路由所需格式
export const generateRouter = (userRouters: any[]) => {
  let newRouter = null
  const routes: any[] = []
  if (userRouters)
    newRouter = userRouters.map((i) => {
      const routesAddr: any = {
        path: `/${i.index}`,
        name: i.name,
        // meta: i.meta,
        component: loadComponent(i.filePath),
      }
      if (i.children) {
        routesAddr.children = generateRouter(i.children)
      }
      routes.push(routesAddr)
    })
  return routes;
}
/**
 * 添加动态路由
 */
export function setAddRoute(routes: any[]) {
  if (routes && routes.length > 0) {
    // const myRouter = router.getRoutes();
    // myRouter[0].children = routes[0].children?.concat(routes as []) as []
    routes.forEach((route) => {
      const routeName = route.name
      if (!router.hasRoute(routeName)) router.addRoute("home", route)
    })
  }
}
