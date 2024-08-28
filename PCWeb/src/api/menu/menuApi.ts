import instance from '@/http/filter';
//获取列表
export const getMenuDataNew = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Basic/Menu/GetMenus', params);
};
//添加
export const addMenu = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Basic/Menu/Add', params);
};
//修改
export const editMenu = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Basic/Menu/Edit', params);
};
//删除
export const delMenu = async (id: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Basic/Menu/Del?id=' + id);
};
//BatchDel
export const batchDelMenu = async (ids: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Basic/Menu/BatchDel?ids=' + ids);
};
//分配菜单
export const settingMenu = async (rid: string, mids: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get(`/api/Basic/Menu/SettingMenu?rid=${rid}&mids=${mids}`);
};
//根据角色获取菜单
export const getUserMenus = async () => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Basic/Menu/GetUserMenus');
};