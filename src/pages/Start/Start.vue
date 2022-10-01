<template src="./Start.html"></template>
<script>
import QuestList from "@/components/QuestList";
import { mapGetters } from "vuex";
export default {
  name: "Quiz",
  data() {
    return {};
  },
  methods: {},
  computed: {
    ...mapGetters(["Questions", "PuginationCounter"]),
  },
  components: {
    QuestList,
  },
};
</script>
<style scoped src="./Start.css"></style>
