import instance from '@/http/filter';
//获取列表
export const getTemplateData = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/WorkflowTemplate/GetTemplateList', params);
};
//添加
export const addTemplate = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.post('/api/WorkflowTemplate/AddTemplate', params);
};
//根据Id获取信息
export const getTemplateInfo = async (params: {}) => {
  instance.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage['token'];
  return instance.get('/api/WorkflowTemplate/GetTemplateById', params);
};