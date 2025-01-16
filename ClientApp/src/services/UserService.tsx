// src/services/EmployeeService.ts

import { UserDTO } from '../models/UserDTO';
import apiClient from './apiClient';

const UserService = {
  async getlist(Page: number = 1) {
    try {
      const response = await apiClient.get('/User/GetList', {
        params: {
          Page
        },
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async AddUser(data: UserDTO) {
    try {
      const response = await apiClient.post(`/User/AddUser`,{
        ...data
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async UpdateUser(id: number,data: UserDTO) {
    try {
      const response = await apiClient.put(`/User/EditUser/${id}`,{
        ...data
      });
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  async DeleteUser(id: number) {
    try {
      const response = await apiClient.delete(`/User/DeleteUser/${id}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};

export default UserService;
