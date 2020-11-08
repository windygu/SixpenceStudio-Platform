<template>
  <a-card :bordered="false">
    <slot name="title">
      <p>通过编辑内容或关键词规则，快速进行自动回复设置。<a-switch default-checked @change="onChange" style="float:right" /></p>
    </slot>
    <sp-editor v-model="data.content"></sp-editor>
  </a-card>
</template>

<script>
export default {
  name: 'focusReply',
  data() {
    return {
      data: {
        content: ''
      },
      controllerName: 'WeChatFocusReply'
    };
  },
  created() {
    this.getData();
  },
  methods: {
    onChange(checked) {
      this.data.checked = checked;
    }
  },
  async getData() {
    return sp.get(`api/${this.controllerName}/GetData`).then(resp => (this.data = resp));
  }
};
</script>

<style></style>
