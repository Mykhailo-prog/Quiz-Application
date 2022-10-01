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
    ...mapActions(["RegisterUser", "resetErrors"]),
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
        this.$router.push("/confirmEmail");
      }
    },
  },
  computed: {
    ...mapGetters(["errorResponse", "RegisterResponse"]),
  },
};
</script>
<style scoped src="./RegisterUser.css"></style>
