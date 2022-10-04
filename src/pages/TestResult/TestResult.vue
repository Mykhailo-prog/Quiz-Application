<template src="./TestResult.html"></template>
<script>
import { mapActions, mapState, mapGetters, mapMutations } from "vuex";
export default {
  name: "TestResult",
  data() {
    return {};
  },
  methods: {
    ...mapActions({}),
    backToTests() {
      this.$router.push("/tests");
    },
  },
  computed: {
    ...mapGetters({
      Result: "TestResult",
      Test: "CurrentTest",
    }),
  },

  components: {},
};
</script>
<style scoped src="./TestResult.css"></style>
