<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="编码" prop="code">
          <el-input v-model="data.code"></el-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="值" prop="value">
          <el-input v-model="data.value"></el-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="描述">
          <el-input v-model="data.description"></el-input>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import edit from '../../../mixins/edit';

export default {
  name: 'sysConfigEdit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'SysConfig',
      rules: {
        name: [{ required: true, message: '请输入参数名', trigger: 'blur' }],
        code: [{ required: true, message: '请输入编码', trigger: 'blur' }],
        value: [{ required: true, message: '请输入值', trigger: 'blur' }]
      }
    };
  }
};
</script>
