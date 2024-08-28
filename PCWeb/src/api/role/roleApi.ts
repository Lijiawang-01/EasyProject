import instance from '@/http/filter';
//获取列表
export const getRoleData = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Role/GetRoles', parms);
};
//添加
export const addRole = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Role/Add', parms);
};
//修改
export const editRole = async (parms: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/Role/Edit', parms);
};
//删除
export const delRole = async (id: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Role/Del?id=' + id);
};
//BatchDel
export const batchDelRole = async (ids: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/Role/BatchDel?ids=' + ids);
};

//分配
export const settingRole = async (pid: string, rids: string) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get(`/api/Users/SettingRole?pid=${pid}&rids=${rids}`);
};