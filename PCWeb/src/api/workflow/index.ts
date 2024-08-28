import http from '@/http/filter'
import { SelectUser } from '@/class/workflow'
import { ref } from 'vue'

const baseUrl = import.meta.env.BASE_URL + "json/"

export const searchVal = ref('')
export const departments = ref({
  titleDepartments: [] as SelectUser[],
  childDepartments: [] as SelectUser[],
  employees: [] as SelectUser[],
})
export const roles = ref({})
export const getRoleList = async () => {
  await getRoles({}).then(res => {
    roles.value = res as any;
  })
}
export const getDepartmentList = async (parentId = 0) => {
  await getDepartments({ parentId }).then(res => {
    departments.value = res as any;
  })
}
export const getDebounceData = (searchVal: string, type = 1) => {
  async () => {
    if (searchVal) {
      const data = {
        searchName: searchVal,
        pageNum: 1,
        pageSize: 30
      }
      if (type == 1) {
        departments.value.childDepartments = [];
        await getEmployees(data).then(res => {
          departments.value.employees = res.data.list
        })

      } else {
        await getRoles(data).then(res => {
          roles.value = res.data.list
        })
      }
    } else {
      type == 1 ? await getDepartmentList() : await getRoleList();
    }
  }
}
/**
 * 获取角色
 * @param {*} data 
 * @returns 
 */
export function getRoles(data: any) {
  return http.get(`${baseUrl}roles.json`, { params: data })
}

/**
 * 获取部门
 * @param {*} data 
 * @returns 
 */
export function getDepartments(data: any) {
  return http.get(`${baseUrl}departments.json`, { params: data })
}

/**
 * 获取职员
 * @param {*} data 
 * @returns 
 */
export function getEmployees(data: any) {
  return http.get(`${baseUrl}employees.json`, { params: data })
}
/**
 * 获取条件字段
 * @param {*} data 
 * @returns 
 */
export function getConditions(data: any) {
  return http.get(`${baseUrl}conditions.json`, { params: data })
}

/**
 * 获取审批数据
 * @param {*} data 
 * @returns 
 */
export function getWorkFlowData(data: any) {
  return http.get(`${baseUrl}data.json`, { params: data })
}
/**
 * 设置审批数据
 * @param {*} data 
 * @returns 
 */
export function setWorkFlowData(data: any) {
  return http.post(`${baseUrl}`, data)
}