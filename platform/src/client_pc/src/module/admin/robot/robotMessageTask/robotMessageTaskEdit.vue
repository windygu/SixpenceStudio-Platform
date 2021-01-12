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
          <sp-select v-model="data.robotid" :options="selectDataList.robot" @change="item => (data.robotidName = item.name)"></sp-select>
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
import { edit, select } from '../../../../mixins';

export default {
  name: 'robot-edit',
  mixins: [edit, select],
  data() {
    return {
      controllerName: 'Robot',
      rules: {
        name: [{ required: true, message: '请输入任务名', trigger: 'blur' }],
        hook: [{ required: true, message: '请输入执行时间', trigger: 'blur' }]
      },
      selectEntityNameList: ['robot']
    };
  }
};
</script>
