<template src="./TestList.html"></template>
<script>
import { mapActions } from "vuex";
import TestForm from "@/components/TestForm";
export default {
  props: {
    test: {
      type: Object,
    },
    userTestCheck: {
      type: Boolean,
    },
  },
  data() {
    return {
      showStatistic: false,
    };
  },
  methods: {
    ...mapActions(["chooseTest"]),
    defineTest() {
      this.chooseTest(this.test.testId);
      this.$router.push("/quiz");
    },
    deleteTest() {
      this.$emit("delete-test", this.test.testId);
      this.$bvModal.hide("delete-" + this.test.testId.toString());
    },
    checkDelete() {},
  },
  components: {
    TestForm,
  },
};
</script>
<style scoped src="./TestList.css"></style>
