<template src="./LoginUser.html"></template>
<script>
import { BIconThreeDotsVertical } from "bootstrap-vue";
import { mapActions, mapGetters } from "vuex";
export default {
  data() {
    return {
      loginForm: {
        login: "",
        password: "",
      },
    };
  },
  methods: {
    ...mapActions(["loginUser"]),
    registerBtn() {
      this.$emit("toRegister");
    },
    async LoginUser() {
      await this.loginUser(this.loginForm);
      if (this.Access.success) {
        this.loginForm = {
          login: "",
          password: "",
        };
        this.$router.push("/tests");
      }
    },
  },
  computed: {
    ...mapGetters(["errorResponse", "Access"]),
  },
};
</script>
<style scoped src="./LoginUser.css"></style>
