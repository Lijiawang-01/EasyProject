import instance from '@/http/filter';
import finstance from '@/http/fileFilter';
//获取列表
export const getUserData = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Users/GetUsers', parms);
};
//添加
export const addUsers = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Users/Add', parms);
};
//修改
export const editUsers = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Users/Edit', parms);
};
//删除
export const delUsers = async (id: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Users/Del?id=' + id);
};
//BatchDel
export const batchDelUsers = async (ids: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Users/BatchDel?ids=' + ids);
};
//个人中心修改用户昵称和密码
export const editNickNameOrPassword = async (nickName: string, password: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get(`/api/Users/EditNickNameOrPassword?nickName=${nickName}&password=${password}`);
};
//修改用户信息
export const editUserFile = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return finstance.post(`/api/Test/Upload`,parms);
};