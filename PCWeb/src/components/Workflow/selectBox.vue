<template>
	<ul class="select-box">
		<template v-for="elem in list">
			<template v-if="elem.type === 'role'">
				<li
					v-for="item in elem.data"
					:key="item.id"
					class="check_box"
					:class="{ active: elem.isActive && elem.isActive(item) }"
					@click="elem.change(item)"
				>
					<a :title="item.description" :class="{ active: elem.isActive && elem.isActive(item) }">
						<img src="@/assets/images/icon_role.png" />{{ item.name }}
					</a>
				</li>
			</template>
			<template v-if="elem.type === 'department'">
				<li v-for="item in elem.data" :key="item.id" class="check_box">
					<a v-if="elem.isDepartment" :class="elem.isActive(item) && 'active'" @click="elem.change(item)">
						<img src="@/assets/images/icon_file.png" />{{ item.name }}</a
					>
					<a v-else><img src="@/assets/images/icon_file.png" />{{ item.name }}</a>
					<i @click="elem.next(item)">下级</i>
				</li>
			</template>
			<template v-if="elem.type === 'employee'">
				<li v-for="item in elem.data" :key="item.id" class="check_box">
					<a :class="elem.isActive(item) && 'active'" @click="elem.change(item)"> <img src="@/assets/images/icon_people.png" />{{ item.name }} </a>
				</li>
			</template>
		</template>
	</ul>
</template>
<script lang="ts" setup>
defineProps({
	list: {
		type: Array<any>,
		default: () => [],
	},
});
</script>
<style lang="scss" scoped>
.select-box {
	height: 420px;
	overflow-y: auto;

	li {
		padding: 5px 0;

		i {
			float: right;
			padding-left: 24px;
			padding-right: 10px;
			color: #3195f8;
			font-size: 12px;
			cursor: pointer;
			background: url('@/assets/images/next_level_active.png') no-repeat 10px center;
			border-left: 1px solid rgb(238, 238, 238);
		}

		a.active + i {
			color: rgb(197, 197, 197);
			background-image: url('@/assets/images/next_level.png');
			pointer-events: none;
		}

		img {
			width: 14px;
			vertical-align: middle;
			margin-right: 5px;
		}
	}
}
</style>
