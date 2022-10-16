<template src="./RegisterUser.html"> </template>
<script>
import { mapActions, mapGetters } from "vuex";

export default {
  data() {
    return {
      registerForm: {
        emailAddress: "",
        login: "",
        password: "",
        confirmPassword: "",
      },
    };
  },
  methods: {
    ...mapActions(["RegisterUser", "resetErrors", "loadUsers"]),
    async Register() {
      await this.RegisterUser(this.registerForm);
      if (this.RegisterResponse.success) {
        this.registerForm = {
          emailAddress: "",
          login: "",
          password: "",
          confirmPassword: "",
        };
        this.resetErrors();
        await this.loadUsers();
        this.$emit("toLogin");
      }
    },
  },
  computed: {
    ...mapGetters(["errorResponse", "RegisterResponse"]),
  },
};
</script>
<style scoped src="./RegisterUser.css"></style>
