<template>
  <sp-list :controllerName="controllerName" :operations="operations" :columns="columns"></sp-list>
</template>

<script>
export default {
  name: 'WeChat',
  data() {
    return {
      controllerName: 'WeChat',
      operations: ['new', 'delete', 'search'],
      columns: [
        { prop: 'name', label: '名称' },
        { prop: 'brief', label: '简介' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' },
        { prop: 'modifiedByName', label: '最后修改人' },
        { prop: 'modifiedOn', label: '最后修改日期', type: 'datetime' }
      ]
    };
  }
};
</script>

<style></style>
