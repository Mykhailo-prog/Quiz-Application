<template src="./TestList.html"></template>
<script>
import { mapActions } from "vuex";
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
      TestId: 0,
      showStatistic: false,
    };
  },
  methods: {
    ...mapActions(["chooseTest"]),
    defineTest() {
      this.$router.push("/quiz");
      this.chooseTest(this.TestId);
      this.$emit("chosen-test");
    },
    deleteTest() {
      this.$emit("delete-test", this.TestId);
      this.$bvModal.hide("delete-" + this.TestId.toString());
    },
    checkDelete() {},
  },
};
</script>
<style scoped src="./TestList.css"></style>
