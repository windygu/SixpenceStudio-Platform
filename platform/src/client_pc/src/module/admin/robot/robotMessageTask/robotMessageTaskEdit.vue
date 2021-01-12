<template>
  <a-form-model ref="form" :model="data" :rules="rules">
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="名称" prop="name">
          <el-input v-model="data.name"></el-input>
        </a-form-model-item>
      </a-col>
      <a-col :span="12">
        <a-form-model-item label="执行时间" prop="hook">
          <el-input v-model="data.hook"></el-input>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="机器人">
          <a-select v-model="data.robotid" @change="handleTypeChange">
            <a-select-option v-model="item.Value" v-for="(item, index) in robotList" :key="index">{{ item.Name }}</a-select-option>
          </a-select>
        </a-form-model-item>
      </a-col>
    </a-row>
    <a-row :gutter="24">
      <a-col :span="12">
        <a-form-model-item label="内容">
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
        name: [{ required: true, message: '请输入任务名', trigger: 'blur' }],
        hook: [{ required: true, message: '请输入执行时间', trigger: 'blur' }]
      },
      robotList: []
    };
  },
  created() {
    this.getRobot();
  },
  methods: {
    handleTypeChange(value) {
      const arrs = this.robotType.filter(item => item.Value === value);
      if (arrs.length > 0) {
        this.data.robotIdName = arrs[0].Name;
      }
    },
    getRobot() {
      sp.get('api/Robot/GetDataList').then(resp => {
        this.robotList = resp.map(item => ({ Name: item.name, Value: item.Id }));
      });
    }
  }
};
</script>
