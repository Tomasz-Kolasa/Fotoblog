<template>
<v-col
    class="d-flex child-flex flex-column mt-3"
    :cols="$vuetify.breakpoint.name == 'xs'|| $vuetify.breakpoint.name =='sm' ? 12 : 6"
>
    <div class="d-flex justify-space-between mb-2 text-subtitle-2">
        <div>
            <v-chip
            class="px-2"
            v-for="tag in photo.tags"
            :key="tag.id">
            {{tag.name}}
            </v-chip>
        </div>
        <div class="text-right">
            <v-chip outlined>{{photo.createdAt.substring(0,10)}}</v-chip>
        </div>
    </div>
    <v-img
    :src="$http.defaults.baseURL + photo.thumbnailUrl"
    :aspect-ratio="16/9"
    class="grey lighten-2"
    >
        <options-bar
            v-if="user.isAdmin"
            @delete-photo="$emit('delete-photo')"
            @edit-photo="$emit('edit-photo')"
        ></options-bar>
        <template v-slot:placeholder>
            <v-row
            class="fill-height ma-0"
            align="center"
            justify="center"
            >
            <v-progress-circular
                indeterminate
                color="grey lighten-5"
            ></v-progress-circular>
            </v-row>
        </template>
    </v-img>
    <div class="text-h5 mt-1 text-center">{{ photo.title }}</div>
    <div class="text-subtitle-1 text-center font-italic">{{photo.description  || "&nbsp;"}}</div>
</v-col>
</template>
<script>
    import OptionsBar from '@/components/photo-item/OptionsBar.vue'
    import { useUserStore } from '@/pinia/stores/useUserStore';

    export default {
        name: 'photo-item',
        props:['photo'],
        components: { 'options-bar': OptionsBar},
        data(){
            return {
                user: useUserStore()
            }
        }
    }
</script>