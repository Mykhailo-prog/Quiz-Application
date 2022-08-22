<template src="./TestList.html"></template>
<script>
import { mapActions } from "vuex";
export default {
  props: {
    test: {
      type: Object,
    },
  },
  data() {
    return {
      TestId: 0,
    };
  },
  methods: {
    ...mapActions(["chooseTest"]),
    defineTest() {
      this.$router.push("/quiz");
      this.chooseTest(this.TestId);
      this.$emit("chosen-test");
    },
  },
};
</script>
<style scoped src="./TestList.css"></style>
