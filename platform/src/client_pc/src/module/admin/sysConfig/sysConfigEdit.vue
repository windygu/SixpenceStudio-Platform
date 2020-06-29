<template>
  <el-form ref="form" :model="data" label-width="80px" :rules="rules">
    <el-row>
      <el-col :span="8">
        <el-form-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item label="编码" prop="code">
          <el-input v-model="data.code"></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-form-item label="值" prop="value">
          <el-input v-model="data.value"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col :span="12">
        <el-form-item label="描述">
          <el-input v-model="data.description"></el-input>
        </el-form-item>
      </el-col>
    </el-row>
    <el-row>
      <el-col style="text-align:right">
        <span class="dialog-footer">
          <el-button @click="$emit('close')">取 消</el-button>
          <el-button type="primary" @click="saveData">确 定</el-button>
        </span>
      </el-col>
    </el-row>
  </el-form>
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
