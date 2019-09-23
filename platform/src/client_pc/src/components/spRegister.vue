<template>
    <el-form ref="form" :model="form" label-width="90px" class="register">
        <span class="header">
            {{header}}
        </span>
        <el-form-item label="昵称：" for="name">
            <el-input v-model="form.name" id="name"></el-input>
        </el-form-item>
        <el-form-item label="邮箱：" for="email">
            <el-input type="email" v-model="form.email" id="email"></el-input>
            
        </el-form-item>
        <el-form-item label="密码：" for="psw">
            <el-input type="password" v-model="form.password" id="psw"></el-input>
        </el-form-item>
        <el-form-item label="确认密码：" for="r_psw">
            <el-input type="password" v-model="form.re_password" id="r_psw"></el-input>
        </el-form-item>
        <el-form-item>
            <el-button style="width=100%" type="primary" @click="check">注册</el-button>
        </el-form-item>
    </el-form>
</template>

<script>
export default {
    name: 'spRegister',
    props:{
        header:{
            type: String,
            default: 'Sign Up'
        }
    },
    data() {
        return{
            form:{
                name: '',
                email: '',
                password: '',
                re_password: ''
            }
        }
    },
    methods: {
        check() {
            let errorMsg = '';
            if(this.form.name === ''){
                errorMsg = '账号不能为空！';
            }
            else if(this.form.email === ''){
                errorMsg = '邮箱不能为空！';                
            }     
            else if (!(/^([a-zA-Z]|[0-9])(\w|\-)+@[a-zA-Z0-9]+\.([a-zA-Z]{2,4})$/.test(this.form.email)))
            {
                errorMsg = '邮箱错误!';
            }       
            else if(this.form.password === ''){
                errorMsg = '密码不能为空！';
            }
            else if(this.form.re_password === ''){
                errorMsg = '确认密码不能为空！';
            }
            else if(this.form.password != this.form.re_password){
                errorMsg = '前后密码不一致！';
            }
            else{
                errorMsg = '注册成功！';
            }
            alert(errorMsg);
        }
        
    }
}
</script>


<style scoped>
.header {
  width: 100%;
  font-size: 50px;
  bottom: 10px;
  position: relative;
}
.register {
  width: 400px;
  height: 400px;
  text-align: center;;
}
</style>
