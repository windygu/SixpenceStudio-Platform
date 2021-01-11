<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="钩子地址" prop="hook">
          <el-input v-model="data.hook"></el-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="类型" prop="robot_type">
          <a-select v-model="data.robot_type" @change="handleTypeChange">
            <a-select-option v-model="item.Value" v-for="(item, index) in robotType" :key="index">{{ item.Name }}</a-select-option>
          </a-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="描述">
          <el-input v-model="data.description"></el-input>
        </a-form-model-item>
      </a-col>
    </a-row>
  </a-form-model>
</template>

<script>
import edit from '../../../../mixins/edit';

export default {
  name: 'robot-edit',
  mixins: [edit],
  data() {
    return {
      controllerName: 'Robot',
      rules: {
        name: [{ required: true, message: '请输入参数名', trigger: 'blur' }],
        hook: [{ required: true, message: '请输入钩子地址', trigger: 'blur' }],
        value: [{ required: true, message: '请输入值', trigger: 'blur' }]
      },
      robotType: []
    };
  },
  created() {
    this.getSysParam();
  },
  methods: {
    handleTypeChange(value) {
      const arrs = this.robotType.filter(item => item.Value === value);
      if (arrs.length > 0) {
        this.data.robot_typename = arrs[0].Name;
      }
    },
    async getSysParam() {
      this.robotType = await sp.get('api/SysParamGroup/GetParams?code=robot_type');
    }
  }
};
</script>
