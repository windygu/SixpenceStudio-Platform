<template>
  <sp-list
    :controller-name="controllerName"
    :operations="operations"
    :columns="columns"
    :editComponent="editComponent"
    :use-pagination="false"
  ></sp-list>
</template>

<script>
import robotMessageTaskEdit from './robotMessageTaskEdit';

export default {
  name: 'robot-message-task-list',
  data() {
    return {
      controllerName: 'RobotMessageTask',
      operations: ['new', 'delete'],
      columns: [
        { prop: 'name', label: '任务名' },
        { prop: 'runtime', label: '执行时间' },
        { prop: 'robotidName', label: '机器人' },
        { prop: 'content', label: '内容' },
        { prop: 'createdByName', label: '创建人' },
        { prop: 'createdOn', label: '创建日期', type: 'datetime' }
      ],
      editComponent: robotMessageTaskEdit
    };
  }
};
</script>
