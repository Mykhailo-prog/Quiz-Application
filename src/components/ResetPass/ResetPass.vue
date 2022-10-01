<template src="./ResetPass.html"> </template>
<script>
import { mapGetters, mapActions } from "vuex";

export default {
  props: {
    queryEmail: {
      type: String,
      required: true,
    },
    queryToken: {
      type: String,
      required: true,
    },
  },
  data() {
    return {
      passwordForm: {
        password: "",
        confirmPassword: "",
        email: "",
        token: "",
      },
    };
  },
  methods: {
    ...mapActions(["resetPassword", "resetErrors"]),
    async ResetPassword() {
      await this.resetPassword(this.passwordForm);
      if (this.ResetPassResponse.success) {
        this.passwordForm = {
          password: "",
          confirmPassword: "",
          email: "",
          token: "",
        };
        this.resetErrors();
        this.$router.push("/");
      }
    },
  },
  computed: {
    ...mapGetters(["errorResponse", "ResetPassResponse"]),
  },
  created() {
    this.passwordForm.email = this.queryEmail;
    this.passwordForm.token = this.queryToken;
  },
};
</script>
<style scoped src="./ResetPass.css"></style>
