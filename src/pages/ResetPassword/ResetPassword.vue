<template src="./ResetPassword.html"> </template>
<script>
import ForgotPassword from "@/components/ForgotPassword";
import ResetPass from "@/components/ResetPass";

export default {
  components: {
    ForgotPassword,
    ResetPass,
  },
  computed: {
    CheckQuery() {
      if (
        this.$router.currentRoute.query.email != undefined &&
        this.$router.currentRoute.query.token != undefined
      ) {
        return true;
      } else {
        return false;
      }
    },
  },
  created() {
    this.CheckQuery;
  },
};
</script>
<style scoped src="./ResetPassword.css"></style>
