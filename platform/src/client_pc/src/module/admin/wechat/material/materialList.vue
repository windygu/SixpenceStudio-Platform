<template>
  <sp-list
    ref="list"
    :controllerName="controllerName"
    :operations="operations"
    :columns="columns"
    :customApi="customApi"
    :headerClick="headerClick"
    :searchList="searchList"
    :editTitle="'详情'"
  >
    <a-form-model
      :model="searchData"
      slot="more"
      layout="horizontal"
      v-bind="{ labelCol: { span: 4 }, wrapperCol: { span: 20 } }"
      label-align="left"
      style="padding: 0 10px"
    >
      <a-row :gutter="24">
        <a-col :span="10">
          <a-form-model-item label="素材类型">
            <a-select v-model="searchData.type">
              <a-select-option v-for="item in materialList" :key="item.Value">
                {{ item.Name }}
              </a-select-option>
            </a-select>
          </a-form-model-item>
        </a-col>
      </a-row>
    </a-form-model>
    <material-read slot="edit" :data="data" :type="searchData.type"></material-read>
  </sp-list>
</template>

<script>
import materialRead from './materialRead';

export default {
  name: 'materialList',
  components: { materialRead },
  data() {
    return {
      controllerName: 'WeChatMaterial',
      operations: ['search', 'more'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'UpdateTime', label: '上传时间', type: 'datetime' }
      ],
      materialList: [],
      searchData: {
        type: 'image'
      },
      data: null
    };
  },
  created() {
    this.getSysParam();
  },
  computed: {
    searchList() {
      return [
        {
          Name: 'type',
          Value: this.searchData.type
        }
      ];
    }
  },
  methods: {
    async getSysParam() {
      this.materialList = await sp.get('api/SysParamGroup/GetParams?code=wechat_material_type');
    },
    headerClick(row) {
      if (row) {
        this.data = row;
      }
      this.$refs.list.editVisible = true;
    }
  }
};
</script>

<style></style>
