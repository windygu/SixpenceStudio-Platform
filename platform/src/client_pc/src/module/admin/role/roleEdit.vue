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
        <a-form-model-item label="权限" prop="privilege">
          <sp-select v-model="data.privilege" :options="selectDataList.privilege" @change="item => (data.privilegeName = item.name)"></sp-select>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import { edit, select } from '../../../mixins';

export default {
  name: 'sysRoleEdit',
  mixins: [edit, select],
  data() {
    return {
      controllerName: 'SysRole',
      selectParamNameList: ['privilege'],
      rules: {
        name: [{ required: true, message: '请输入名称', trigger: 'blur' }],
        privilege: [{ required: true, message: '请选择权限', trigger: 'blur' }]
      }
    };
  }
};
</script>
