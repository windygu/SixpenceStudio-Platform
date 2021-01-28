<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="描述">
          <el-input v-model="data.description"></el-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row>
      <a-col :span="12">
        <a-form-model-item label="是否基础角色">
          <sp-switch v-model="data.is_basic" disabled />
        </a-form-model-item>
      </a-col>
      <a-col :span="12" v-if="!data.is_basic || data.is_basic == 0">
        <a-form-model-item label="继承角色">
          <sp-select v-model="data.parent_roleid" :options="roles" @change="item => (data.parent_roleidName = item.name)"></sp-select>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit } from '../../../mixins';

export default {
  name: 'sysRoleEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysRole',
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }]
      },
      roles: [],
      data: {
        is_basic: 0
      }
    };
  },
  created() {
    sp.get('api/SysRole/GetBasicRole').then(resp => {
      this.roles = resp;
    });
  }
};
</script>
